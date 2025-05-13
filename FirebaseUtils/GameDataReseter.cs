using IJunior.TypedScenes;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameDataReseter : MonoBehaviour
{
    public void Click() => StartCoroutine(ClearData());

    private IEnumerator ClearData()
    {
        PlayerPrefs.DeleteAll();
        ClearPersistentData();
        yield return new WaitForSeconds(2);
        MainMenu.Load();
    }

    private void ClearPersistentData()
    {
        string path = Application.persistentDataPath;

        if (Directory.Exists(path))
        {
            // Удалить все файлы
            foreach (string file in Directory.GetFiles(path))
                File.Delete(file);
            

            // Удалить все папки
            foreach (string directory in Directory.GetDirectories(path))
                Directory.Delete(directory, true);
            
            Debug.Log("All data in persistentDataPath has been deleted.");
        }
        else
            Debug.LogWarning("persistentDataPath does not exist: " + path);
        
    }
}
