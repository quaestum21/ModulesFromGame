using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
/// <summary>
/// Класс для создания файлов Json которые отвечают за прогресс игрока(лут, уровни и etc..)
/// </summary>
public static class InitialClientFilesChecker 
{
    public static IEnumerator CheckClientDefaultFiles()
    {
        yield return CheckAndCreateFile("PlayerStats.json", FillPlayerStats);
        yield return CheckAndCreateFile("Inventory.json", FillInventoryFromDefaultItems);
        yield return CheckAndCreateFile("Garage.json", FillGarageFromDefaultItems);
        yield return CheckAndCreateFile("ItemsInSlots.json", FillItemsInSlots);
        yield return CheckAndCreateFile("LevelScores.json", FillLevelScores);
        yield return CheckAndCreateFile("LevelBonusScores.json", FillLevelScores);
        yield return CheckAndCreateFile("FabricLevels.json", FillFabricLevels);
        yield return CheckAndCreateFile("SelectedTransport.json", FillSelectedTransport);

        PlayerPrefs.SetInt("FirstEnterInGame", 1);
        PlayerPrefs.Save();
    }
    private static IEnumerator CheckAndCreateFile(string fileName, Func<string, IEnumerator> fillMethod)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath))
        {
            FileUtils.CreateFile(filePath);
            yield return fillMethod(filePath);
        }
    }
    #region Методы заполнения
    private static IEnumerator FillPlayerStats(string fileName)
    {
        File.Copy(Path.Combine(Application.persistentDataPath, "PlayerStatsDefault.json"), fileName, true);
        Debug.Log("Файл PlayerStats.json создан");
        yield return null;
    }
    private static IEnumerator FillInventoryFromDefaultItems(string fileName)
    {
        string defaultItemsPath = Path.Combine(Application.persistentDataPath, "DefaultItemInInventory.json");
        if (!File.Exists(defaultItemsPath))
        {
            Debug.LogError("DefaultItemInInventory.json not found.");
            yield break;
        }

        string defaultItemsJson = File.ReadAllText(defaultItemsPath);
        var defaultItems = JObject.Parse(defaultItemsJson);

        var inventoryJson = new JObject();
        if (defaultItems["default_items_in_inventory"] is JValue defaultItemsValue &&
            defaultItemsValue.Type == JTokenType.String)
        {
            var defaultItemsObject = JObject.Parse(defaultItemsValue.ToString());
            foreach (var item in defaultItemsObject)
            {
                if (item.Value is JArray values && values.Count >= 5)
                    inventoryJson[item.Key] = values;
                else
                    Debug.LogError($"Invalid data format for key {item.Key} in DefaultItemInInventory.json.");
            }
        }
        else Debug.LogError("Invalid data format for key default_items_in_inventory in DefaultItemInInventory.json.");

        var fullInventoryJson = new JObject { ["Inventory"] = inventoryJson };
        File.WriteAllText(fileName, fullInventoryJson.ToString());
        Debug.Log("Inventory.json filled with data from DefaultItemInInventory.json.");
        yield return null;
    }
    private static IEnumerator FillGarageFromDefaultItems(string fileName)
    {
        string defaultItemsPath = Path.Combine(Application.persistentDataPath, "DefaultItemsInGarage.json");
        if (!File.Exists(defaultItemsPath))
        {
            Debug.LogError("DefaultItemsInGarage.json not found.");
            yield break;
        }

        string defaultItemsJson = File.ReadAllText(defaultItemsPath);
        var defaultItems = JObject.Parse(defaultItemsJson);

        var inventoryJson = new JObject();
        if (defaultItems["default_items_in_garage"] is JValue defaultItemsValue &&
            defaultItemsValue.Type == JTokenType.String)
        {
            var defaultItemsObject = JObject.Parse(defaultItemsValue.ToString());
            foreach (var item in defaultItemsObject)
            {
                if (item.Value is JArray values && values.Count >= 5)
                    inventoryJson[item.Key] = values;
                else
                    Debug.LogError($"Invalid data format for key {item.Key} in DefaultItemsInGarage.json.");
            }
        }
        else Debug.LogError("Invalid data format for key default_items_in_garage in DefaultItemsInGarage.json.");

        var fullGarageJson = new JObject { ["Garage"] = inventoryJson };
        File.WriteAllText(fileName, fullGarageJson.ToString());
        Debug.Log("Garage.json filled with data from DefaultItemsInGarage.json.");
        yield return null;
    }
    private static IEnumerator FillItemsInSlots(string fileName)
    {
        File.Copy(Path.Combine(Application.persistentDataPath, "DefaultItemsInSlots.json"), fileName, true);
        Debug.Log("Файл DefaultItemsInSlots.json создан");
        yield return null;
    }
    private static IEnumerator FillLevelScores(string fileName)
    {
        var levelScores = new JObject { ["1"] = 0 };
        File.WriteAllText(fileName, levelScores.ToString());
        Debug.Log(fileName + " filled with initial data.");
        yield return null;
    }
    private static IEnumerator FillFabricLevels(string fileName)
    {
        var fabricLevels = new JObject
        {
            ["GeneratorLevels"] = CreateFactoryLevels(),
            ["SteelFactoryLevels"] = CreateFactoryLevels(),
            ["GasFactoryLevels"] = CreateFactoryLevels()
        };

        File.WriteAllText(fileName, fabricLevels.ToString());
        Debug.Log("FabricLevels.json filled with initial data.");
        yield return null;
    }
    private static JObject CreateFactoryLevels()
    {
        return new JObject
        {
            ["Capacity"] = 1,
            ["PerHour"] = 1,
            ["Efficiency"] = 1
        };
    }

    private static IEnumerator FillSelectedTransport(string fileName)
    {
        string garagePath = Path.Combine(Application.persistentDataPath, "Garage.json");

        if (!File.Exists(garagePath))
        {
            Debug.LogError("Garage.json not found.");
            yield break;
        }

        try
        {
            string garageJson = File.ReadAllText(garagePath);
            var garageData = JObject.Parse(garageJson);

            // Получаем первый элемент из Garage
            var firstGarageItem = garageData["Garage"]?.First as JProperty;

            if (firstGarageItem != null)
            {
                // Создаем объект с первой записью
                var selectedTransport = new JObject
                {
                    ["SelectedTransport"] = new JObject
                    {
                        [firstGarageItem.Name] = firstGarageItem.Value
                    }
                };

                File.WriteAllText(fileName, selectedTransport.ToString());
                Debug.Log("SelectedTransport.json filled with first item from Garage.json");
            }
            else
            {
                Debug.LogWarning("No items found in Garage.json");
                // Создаем пустой файл, если в гараже нет элементов
                File.WriteAllText(fileName, new JObject { ["SelectedTransport"] = new JObject() }.ToString());
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error filling SelectedTransport.json: " + e.Message);
            // Создаем пустой файл в случае ошибки
            File.WriteAllText(fileName, new JObject { ["SelectedTransport"] = new JObject() }.ToString());
        }
        yield return null;
    }
    #endregion
}
