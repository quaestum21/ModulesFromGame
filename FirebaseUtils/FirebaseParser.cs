using Firebase.RemoteConfig;
using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
/// <summary>
/// Class designed to take all parameters from firebase,
/// Takes parameters if local numberOfUpdate and remote do not match
/// </summary>
public static class FirebaseParser
{
    //file name to save data from firebase
    private const string _jsonFileName = "FirebaseRemoteConfigBaseData.json";
    private const string numberOfUpdate = "number_of_update";

    //parameters that need to be taken
    private static readonly string[] _valueForExport =
    {
        "warhouse_characteristics",
        "weapons_characteristics",
        "default_items_in_inventory",
        "default_items_in_slots",
        "base_default_stat",
        "levels_stat",
        "battery",
        "fx",
        "gas",
        "hp",
        "hp_regen",
        "metal",
        "oil",
        "power",
        "steel",
        "xp",
        "level_complete_reward",
        "show_blocker",
        "start_coins",
        "test",
        "stat_retake",
        "weapons_upgrade_formulas",
        "mob_stat",
        "levels_stat_bonus",
        "alternatingBonusLevel",
        "supplies_characteristics",
        "ldc",
        "ldcb",
        "map_objects",
        "car_upgrade_formulas",
        "transport_characteristics",
        "default_items_in_garage"
    };
    public static IEnumerator ExtractJsonFile(Action<bool> onComplete, bool forceUpdate = false)
    {
        if (!PlayerPrefs.HasKey(numberOfUpdate))
            PlayerPrefs.SetString(numberOfUpdate, "null");

        bool isSuccess = false;
        var fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);

        yield return new WaitUntil(() => fetchTask.IsCompleted || fetchTask.IsFaulted);

        if (fetchTask.IsCompleted)
        {
            var activateTask = FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

            yield return new WaitUntil(() => activateTask.IsCompleted || activateTask.IsFaulted);

            if (activateTask.IsCompleted)
            {
                string remoteNumberOfUpdate = FirebaseRemoteConfig.DefaultInstance.GetValue(numberOfUpdate).StringValue;
                string localNumberOfUpdate = PlayerPrefs.GetString(numberOfUpdate);

                if (remoteNumberOfUpdate != localNumberOfUpdate || forceUpdate)
                {
                    StringBuilder jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("{\n");

                    for (int i = 0; i < _valueForExport.Length; i++)
                    {
                        string key = _valueForExport[i];
                        string value = FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue.Replace("\"", "\\\"");
                        jsonBuilder.Append($"\t\"{key}\": \"{value}\"");
                        if (i < _valueForExport.Length - 1) jsonBuilder.Append(",\n");
                    }

                    jsonBuilder.Append("\n}\n");

                    SaveJsonToFile(jsonBuilder.ToString(), Path.Combine(Application.persistentDataPath, _jsonFileName));
                    PlayerPrefs.SetString(numberOfUpdate, remoteNumberOfUpdate);
                    PlayerPrefs.Save();
                    isSuccess = true;
                }
                else Debug.Log("No update needed.");
            }
            else Debug.LogError("Failed to activate Firebase Remote Config.");
        }
        else Debug.LogError("Failed to fetch Firebase Remote Config.");

        onComplete?.Invoke(isSuccess);
    }

    private static void SaveJsonToFile(string jsonData, string savePath)
    {
        try
        {
            File.WriteAllText(savePath, jsonData);
            Debug.Log("JSON file saved successfully at: " + savePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save JSON file: " + e.Message);
        }
    }
}
