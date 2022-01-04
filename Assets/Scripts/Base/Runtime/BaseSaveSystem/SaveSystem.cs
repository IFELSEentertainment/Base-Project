using System;
using System.Collections;
using UnityEngine;
namespace Base {
    public static class SaveSystem {

        public static SaveObject GetSaveObject<T>(this T saveObject) where T : Enum {
            return Base_GameManager.instance.Save.GetSaveObject(saveObject);
        }
        
        public static object GetData<T>(this T saveName) where T : Enum {
            return Base_GameManager.instance.Save.GetData(saveName);
        }

        public static string DataToString<T>(this T saveName) where T : Enum {
            return Base_GameManager.instance.Save.GetData(saveName).ToString();
        }
        
        public static int ToInt<T>(this T saveName) where T : Enum {
            return int.Parse($"{Base_GameManager.instance.Save.GetData(saveName).ToString()}");
        }
        
        public static float ToFloat<T>(this T saveName) where T : Enum {
            return float.Parse(Base_GameManager.instance.Save.GetData(saveName).ToString());
        }

        public static void SetData<T>(this T saveName, object value) where T : Enum  {
            Base_GameManager.instance.Save.GetSaveObject(saveName).SetData(saveName, value);
        }
    }
}