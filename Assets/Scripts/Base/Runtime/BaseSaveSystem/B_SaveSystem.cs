﻿using System;
using System.Collections;
using UnityEngine;
namespace Base {
    public static class B_SaveSystem {

        public static B_SaveObject GetSaveObject<T>(this T saveObject) where T : Enum {
            return B_GameManager.instance.bSave.GetSaveObject(saveObject);
        }
        
        public static object GetData<T>(this T saveName) where T : Enum {
            return B_GameManager.instance.bSave.GetData(saveName);
        }

        public static string DataToString<T>(this T saveName) where T : Enum {
            return B_GameManager.instance.bSave.GetData(saveName).ToString();
        }
        
        public static int ToInt<T>(this T saveName) where T : Enum {
            return int.Parse($"{B_GameManager.instance.bSave.GetData(saveName).ToString()}");
        }
        
        public static float ToFloat<T>(this T saveName) where T : Enum {
            return float.Parse(B_GameManager.instance.bSave.GetData(saveName).ToString());
        }

        public static void SetData<T>(this T saveName, object value) where T : Enum  {
            B_GameManager.instance.bSave.GetSaveObject(saveName).SetData(saveName, value);
        }
    }
}