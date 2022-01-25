using System.Collections;
using System.Collections.Generic;
using Base;
using UnityEditor;
#if UNITY_EDITOR


using UnityEngine;


public class EditorCleanup 
{
    [InitializeOnEnterPlayMode]
    static void test() {
        GameObject.Find("LevelHolder").transform.GetComponent<B_LevelCreator>().Clear();
    }
    
}
  #endif