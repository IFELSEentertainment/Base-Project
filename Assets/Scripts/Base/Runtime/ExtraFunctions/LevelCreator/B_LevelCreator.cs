using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities;
using UnityEditor.SceneManagement;
#endif

public class B_LevelCreator : MonoBehaviour {
    #if UNITY_EDITOR
    
    [BoxGroup("Level Creator")]
    [ValueDropdown("GetLevels")]
    [OnValueChanged("LoadLevel")]
    public string SelectedLevel;
    
    private GameObject currentLevel;
    

    List<string> GetLevels() {
        var Levels = new List<string>();
        Levels.Add("Null");
        var levels = Resources.LoadAll(B_Database_String.Path_Res_MainLevels);
        for (int i = 0; i < levels.Length; i++) {
            Levels.Add(levels[i].name);
        }
        return Levels;
    }

    void LoadLevel() {
        if(SelectedLevel.IsNullOrWhitespace()) return;
        if (SelectedLevel == "Null") {
            SaveChanges();
            Clear();
            return;
        }
        if (currentLevel) {
            SaveChanges();
            Clear();
        }
        GameObject obj = Resources.Load<GameObject>(B_Database_String.Path_Res_MainLevels + SelectedLevel);
        currentLevel = PrefabUtility.InstantiatePrefab(obj, transform) as GameObject;
        AssetDatabase.SaveAssets();
    }
    [BoxGroup("Level Creator")]
    [Button]
    void CreateNewLevel() {
        if(SelectedLevel.IsNullOrWhitespace()) return;
        if (currentLevel) {
            SaveChanges();
            Clear();
        }
        GameObject obj = Resources.Load<GameObject>(B_Database_String.Path_Res_MainLevels + SelectedLevel);
        int CurrentLevelsCount = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_MainLevels).Length;
        string newLevelName = (CurrentLevelsCount + 1).ToString();
        if (newLevelName.Length < 3) {
            newLevelName = newLevelName.Insert(0, "0");
            if (newLevelName.Length < 3) {
                newLevelName = newLevelName.Insert(0, "0");
            }
        }
        newLevelName = newLevelName.Insert(0, "MainLevel ");
        currentLevel = Instantiate(obj, transform);
        currentLevel.name = newLevelName;
        PrefabUtility.SaveAsPrefabAsset(currentLevel, $"Assets/Resources/Levels/Mainlevels/{currentLevel.name}.prefab");
        GameObject newObj = Resources.Load<GameObject>(B_Database_String.Path_Res_MainLevels + newLevelName);
        DestroyImmediate(currentLevel);
        currentLevel = PrefabUtility.InstantiatePrefab(newObj, transform) as GameObject;
        SelectedLevel = newLevelName;
        AssetDatabase.SaveAssets();
    }
    
    [BoxGroup("Level Creator")]
    [Button]
    void SaveChanges() {
        if(!currentLevel) return;
        PrefabUtility.ApplyPrefabInstance(currentLevel, InteractionMode.UserAction);
        AssetDatabase.SaveAssets();
    }
    
    [BoxGroup("Level Creator")]
    [Button]
    void ResetChanges() {
        if(!currentLevel) return;
        PrefabUtility.RevertPrefabInstance(currentLevel, InteractionMode.UserAction);
        AssetDatabase.SaveAssets();
    }
    
    [BoxGroup("Level Creator")]
    [Button]
    public void Clear() {
        transform.DestroyAllChildren();
        AssetDatabase.SaveAssets();
    }
    #endif
}
