using System;
using System.Threading.Tasks;
using Base.UI;
using UnityEngine;
namespace Base {
    public enum GameStates { Init, Start, Paused, Playing, End }

    public class Base_GameManager : B_M_ManagerBase {
        
        public static Base_GameManager instance;
        //Fires when the game state changes
        public static Action OnGameStateChange;
        //Stores the actual game state
        private GameStates _currentGameState;
        //Controls the save system
        public SaveSystemEditor Save;
        public GameStates CurrentGameState {
            get => _currentGameState;
            set {
                if (_currentGameState == value) return;
                _currentGameState = value;
                B_CES_CentralEventSystem.OnGameStateChange.InvokeEvent();
            }
        }

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            Save = new SaveSystemEditor();
            GUIManager.GetButton(Enum_Menu_MainComponent.BTN_Start).AddFunction(StartGame);
            GUIManager.GetButton(Enum_Menu_GameOverComponent.BTN_Sucess).AddFunction(EndLevel);
            GUIManager.GetButton(Enum_Menu_GameOverComponent.BTN_Fail).AddFunction(RestartLevel);

            return base.ManagerStrapping();
        }
        public override Task ManagerDataFlush() {
            instance = null;
            return base.ManagerDataFlush();
        }

        public bool IsGamePlaying() {
            if (CurrentGameState == GameStates.Playing) return true;
            return false;
        }

        #region Game Management Functions

        /// <summary>
        /// Starts the game changing the UI and setting the game state to GameStates.Playing
        /// </summary>
        private void StartGame() {
            B_CES_CentralEventSystem.BTN_OnStartPressed.InvokeEvent();
            instance.CurrentGameState = GameStates.Playing;
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_PlayerOverlay);
        }

        /// <summary>
        /// Reloads the game without saving any data
        /// </summary>
        private void RestartLevel() {
            B_CES_CentralEventSystem.BTN_OnRestartPressed.InvokeEvent();
            B_LC_LevelManager.instance.ReloadCurrentLevel();
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main, .3f);
        }
        /// <summary>
        /// Ends the level on press of a button
        /// </summary>
        private void EndLevel() {
            B_CES_CentralEventSystem.BTN_OnEndGamePressed.InvokeEvent();
            instance.CurrentGameState = GameStates.Start;
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main);
            B_LC_LevelManager.instance.LoadInNextLevel();
        }

        /// <summary>
        /// Activates the end game, changes game state and fires signals
        /// </summary>
        /// <param name="Success"></param>
        /// Set true for if player won, false if player lost
        /// <param name="Delay"></param>
        /// Set time for the delay on UI. Doesn't effects signals
        public async void ActivateEndgame(bool Success, float Delay = 0) {
            if (CurrentGameState == GameStates.End || CurrentGameState == GameStates.Start) return;
            instance.CurrentGameState = GameStates.End;
            switch (Success) {
                case true:
                    B_CES_CentralEventSystem.OnBeforeLevelDisablePositive.InvokeEvent();
                    break;
                case false:
                    B_CES_CentralEventSystem.OnBeforeLevelDisableNegative.InvokeEvent();
                    break;
            }
            await Task.Delay((int)Delay * 1000);
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_GameOver);
            GUIManager.GameOver.EnableOverUI(Success);
            Save.SaveAllData();
        }

        //Uncomment these functions if you want game to save data on pause or quit

        // private void OnApplicationPause(bool pause) {
        //     Save.SaveAllData();
        // }
        //
        // private void OnApplicationQuit() {
        //     Save.SaveAllData();
        // }

        #endregion
    }


    class MyClass {
        void MyFunction() {
            Base_GameManager.instance.ActivateEndgame(true,3f);
        }
    }
    
    
}