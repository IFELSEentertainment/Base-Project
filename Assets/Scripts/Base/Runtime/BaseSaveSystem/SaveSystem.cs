namespace Base {
    public static class SaveSystem {

        public static object GetDataObject(object saveName, object saveEnum) {
            if (string.IsNullOrEmpty((string)Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum))) return null;
            return Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum);
        }

        public static string GetDataString(object saveName, object saveEnum) {
            if (string.IsNullOrEmpty((string)Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum))) return null;
            return Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum).ToString();
        }

        public static int GetDataInt(object saveName, object saveEnum) {
            return int.Parse(string.Format("{0}", Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum)));
        }

        public static float GetDataFloat(object saveName, object saveEnum) {
            return float.Parse(Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum).ToString());
        }

        public static void SetData(object saveName, object saveEnum, object DataToSave) {
            Base_GameManager.instance.Save.GetSaveObject(saveName.ToString()).SetData(saveEnum, DataToSave);
        }
    }
}