using System.Collections.Generic;
using Base.UI;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace Base {
    [DefaultExecutionOrder(-100)]
    public class Base_BootLoader : MonoBehaviour {

        #region Spesific Functions
        /// <summary>
        /// Set to -1 to get unlimited frame rate
        /// </summary>
        [TabGroup("Master", "Game Aspect Control")]
        public int TargetFrameRate = 60;
        /// <summary>
        /// Prepares every aspect of the Base Engine
        /// Loads and sets up managers, level loading system, save system etc.
        /// </summary>
        private async void InitiateBootLoading() {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
            await B_CES_CentralEventSystem.CentralEventSystemStrapping();
            for (var i = 0; i < Managers.Count; i++) await Managers[i].ManagerStrapping();
            if (!HasTutorial) SaveSystem.SetData(Enum_MainSave.TutorialPlayed, 1);
            await VfmEffectsManager.VFXManagerStrapping();
            await EffectsManager.EffectsManagerStrapping();

            Base_GameManager.instance.CurrentGameState = GameStates.Start;

            B_LC_LevelManager.instance.LoadInLevel(Enum_MainSave.PlayerLevel.GetDataInt());
            Base_GameManager.instance.Save.SaveAllData();
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main, .2f);
            
            IngameEditor = GetComponent<EditorInGame>();
            IngameEditor.enabled = RuntimeEditor;

            if (TargetFrameRate < 0) {
                TargetFrameRate = -1;
            }
            
        }

        #endregion

#if UNITY_EDITOR
        #region Editor Functions
        /// <summary>
        /// Sets up managers and creates enums from their names
        /// </summary>
        [TabGroup("Master", "Editor Control")]
        [Button]
        public void SetupManagerEnums() {
            var names = new string[Managers.Count];
            for (var i = 0; i < Managers.Count; i++) names[i] = Managers[i].GetType().Name;
            EnumCreator.CreateEnum("Managers", names);
        }

        #endregion
#endif

        #region Properties
        [TabGroup("Master", "Game Aspect Control")]
        [SerializeField] private bool HasTutorial;
        [TabGroup("Master", "Editor Control")]
        [SerializeField] private List<B_M_ManagerBase> Managers;
        [TabGroup("Master", "Editor Control")]
        [SerializeField] private B_VFM_EffectsManager VfmEffectsManager; 
        private EditorInGame IngameEditor;
        [TabGroup("Master", "Editor Control")]
        [SerializeField] private bool RuntimeEditor;

        #endregion

        #region Unity Functions

        private void Awake() {

#if UNITY_IOS
            //Asks for permission on iOS devices
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED || ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.RESTRICTED || ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.DENIED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
#else

#endif
            InitiateBootLoading();
        }

        private void OnDisable() {
            for (var i = 0; i < Managers.Count; i++) Managers[i].ManagerDataFlush();
        }

        #endregion
    }
}