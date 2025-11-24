using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(SaveDatarda data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static SaveDatarda Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveDatarda>(json);
        }
        else
        {
            // si no hay uno, se crea
            return new SaveDatarda();
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Archivo de guardado eliminado.");
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Datos borrados exitosamente.");
    }
}
