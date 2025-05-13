using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// This class splits a previously parsed json file
/// </summary>
public static class FirebaseFileSplitter
{
    private static readonly Dictionary<int, Func<string, BaseValuesFromFirebaseRemoteConfig, IEnumerator>> _fillMethods =
        new Dictionary<int, Func<string, BaseValuesFromFirebaseRemoteConfig, IEnumerator>>
    {
        { 0, FillPlayerStats },
        { 1, FillWeaponsStats },
        { 2, FillWarHouseStats },
        { 3, FillDefaultItemsInSlots },
        { 4, FillDefaultItemsInInventory },
        { 5, FillLevelsStat },
        { 6, (fileName, baseParams) => FillBaseDefaultStat(fileName, baseParams.base_default_stat) },
        { 7, FillWeaponsUpgradeFormulas },
        { 8, FillMobStats },
        { 9, FillLevelsBonusStat },
        { 10, FillAlternatingBonusLevel },
        { 11, FillSuppliesStats },
        { 12, FillLdcb },
        { 13, FillLdc },
        { 14, FillMapObjects },
        { 15, FillCarFormulas },
        { 16, FillTransportCharacteristics },
        { 17, FillDefaultItemsInGarage }
    };
    private static readonly string[] _clientFileNames =
    {
        "PlayerStatsDefault.json",
        "WeaponsCharacteristics.json",
        "WarhouseCharacteristics.json",
        "DefaultItemsInSlots.json",
        "DefaultItemInInventory.json",
        "LevelsStat.json",
        "BaseDefaultStat.json",
        "WeaponsUpgradeFormulas.json",
        "MobStats.json",
        "LevelsBonusStat.json",
        "AlternatingBonusLevel.json",
        "SupplesCharacterictics.json",
        "ldcb.json",
        "ldc.json",
        "MapObjects.json",
        "CarUpgradeFormulas.json",
        "TransportCharacteristics.json",
        "DefaultItemsInGarage.json"
    };
    public static IEnumerator Split(bool useStatRetake)
    {
        string baseFile = Path.Combine(Application.persistentDataPath, "FirebaseRemoteConfigBaseData.json");
        yield return useStatRetake ?
            CheckAndFillStatRetake(baseFile, true) :
            ProcessAllFiles(baseFile);
    }

    private static IEnumerator ProcessAllFiles(string baseFileName)
    {
        if (!File.Exists(baseFileName))
        {
            Debug.LogError("File baseparameters.json not found.");
            yield break;
        }

        for (int i = 0; i < _clientFileNames.Length; i++)
        {
            string pathFile = Path.Combine(Application.persistentDataPath, _clientFileNames[i]);
            if (!File.Exists(pathFile))
                FileUtils.CreateFile(pathFile);
            yield return FillingFileWithStandartParameters(_clientFileNames[i], i, baseFileName);
        }
    }

    private static IEnumerator FillingFileWithStandartParameters(string fileName, int index, string baseFileName)
    {
        string baseParametersJson = File.ReadAllText(baseFileName);
        var baseParameters = JsonUtility.FromJson<BaseValuesFromFirebaseRemoteConfig>(baseParametersJson);

        if (_fillMethods.TryGetValue(index, out var fillMethod))
            yield return fillMethod(fileName, baseParameters);
        else
            Debug.LogError("No fill method found for index: " + index);
    }

    private static IEnumerator CheckAndFillStatRetake(string baseFileName, bool statusOfRetake)
    {
        if (!File.Exists(baseFileName))
        {
            Debug.LogError("File baseparameters.json not found.");
            yield break;
        }

        string baseParametersJson = File.ReadAllText(baseFileName);
        var baseParameters = JsonUtility.FromJson<BaseValuesFromFirebaseRemoteConfig>(baseParametersJson);

        if (statusOfRetake) //determines whether to update files of a specific group or all at once
        {
            var statRetake = JsonConvert.DeserializeObject<StatRetake>(baseParameters.stat_retake);

            if (statRetake.base_stat_retake)
                yield return FillBaseDefaultStat("BaseDefaultStat.json", baseParameters.base_default_stat);

            if (statRetake.mob_stat_retake)
                yield return FillMobStats("MobStats.json", baseParameters);

            if (statRetake.items_stat_retake)
            {
                yield return FillWeaponsStats("WeaponsCharacteristics.json", baseParameters);
                yield return FillWarHouseStats("WarhouseCharacteristics.json", baseParameters);
                yield return FillSuppliesStats("SuppliesCharacteristics.json", baseParameters);
                yield return FillDefaultItemsInSlots("DefaultItemsInSlots.json", baseParameters);
                yield return FillDefaultItemsInGarage("DefaultItemsInGarage.json", baseParameters);
                yield return FillDefaultItemsInInventory("DefaultItemInInventory.json", baseParameters);
                yield return FillWeaponsUpgradeFormulas("WeaponsUpgradeFormulas.json", baseParameters);
                yield return FillCarFormulas("CarUpgradeFormulas.json", baseParameters);
                yield return FillTransportCharacteristics("TransportCharacteristics.json", baseParameters);
            }

            if (statRetake.levels_stat_retake)
            {
                yield return FillLevelsStat("LevelsStat.json", baseParameters);
                yield return FillLevelsBonusStat("LevelsBonusStat.json", baseParameters);
                yield return FillAlternatingBonusLevel("AlternatingBonusLevel.json", baseParameters);
                yield return FillLdc("ldc.json", baseParameters);
                yield return FillLdcb("ldcb.json", baseParameters);
                yield return FillMapObjects("MapObjects.json", baseParameters);
            }

            if (statRetake.player_stat_retake)
                yield return FillPlayerStats("PlayerStatsDefault.json", baseParameters);
        }
        else
            yield return FillAllFiles(baseParameters);
    }
    private static IEnumerator FillAllFiles(BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        yield return FillBaseDefaultStat("BaseDefaultStat.json", baseParameters.base_default_stat);
        yield return FillWeaponsStats("WeaponsCharacteristics.json", baseParameters);
        yield return FillWarHouseStats("WarhouseCharacteristics.json", baseParameters);
        yield return FillSuppliesStats("SuppliesCharacteristics.json", baseParameters);
        yield return FillDefaultItemsInInventory("DefaultItemInInventory.json", baseParameters);
        yield return FillDefaultItemsInGarage("DefaultItemsInGarage.json", baseParameters);
        yield return FillLevelsStat("LevelsStat.json", baseParameters);
        yield return FillPlayerStats("PlayerStatsDefault.json", baseParameters);
        yield return FillWeaponsUpgradeFormulas("WeaponsUpgradeFormulas.json", baseParameters);
        yield return FillMobStats("MobStats.json", baseParameters);
        yield return FillLevelsBonusStat("LevelsBonusStat.json", baseParameters);
        yield return FillAlternatingBonusLevel("AlternatingBonusLevel.json", baseParameters);
        yield return FillLdc("ldc.json", baseParameters);
        yield return FillLdcb("ldcb.json", baseParameters);
        yield return FillMapObjects("MapObjects.json", baseParameters);
        yield return FillCarFormulas("CarUpgradeFormulas.json", baseParameters);
        yield return FillTransportCharacteristics("TransportCharacteristics.json", baseParameters);
    }
    private static void SaveJson<T>(string fileName, T data) where T : class
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), JsonUtility.ToJson(data, true));
        Debug.Log("File filled with standard parameters: " + fileName);
    }

    private static void SaveFormattedJson<T>(string fileName, T data)
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName),
            JsonConvert.SerializeObject(data, Formatting.Indented));
        Debug.Log("File filled with formatted data: " + fileName);
    }
    #region Методы заполнения
    private static IEnumerator FillMobStats(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var mobsStatDict = JsonConvert.DeserializeObject<Dictionary<string, MobStat>>(baseParameters.mob_stat);
        SaveFormattedJson(fileName, mobsStatDict);
        yield return null;
    }
    private static IEnumerator FillTransportCharacteristics(string fileName, BaseValuesFromFirebaseRemoteConfig baseValues)
    {
        var transportStats = new TransportCharacteristics { transport_characteristics = baseValues.transport_characteristics };
        SaveJson(fileName, transportStats);
        yield return null;
    }
    private static IEnumerator FillCarFormulas(string fileName, BaseValuesFromFirebaseRemoteConfig baseValues)
    {
        var carUpgradeFormulas = JsonConvert.DeserializeObject<CarUpgradeFormulas>(baseValues.car_upgrade_formulas);
        SaveFormattedJson(fileName, carUpgradeFormulas);
        yield return null;
    }
    private static IEnumerator FillMapObjects(string fileName, BaseValuesFromFirebaseRemoteConfig baseValues)
    {
        var mapObjectsWrapper = JsonConvert.DeserializeObject<MapObjectsWrapper>(baseValues.map_objects);
        SaveFormattedJson(fileName, mapObjectsWrapper);
        yield return null;
    }
    private static IEnumerator FillLevelsStat(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var levelsStatDict = JsonConvert.DeserializeObject<Dictionary<string, LevelsStat>>(baseParameters.levels_stat);
        SaveFormattedJson(fileName, levelsStatDict);
        yield return null;
    }
    private static IEnumerator FillLevelsBonusStat(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var levelsStatDict = JsonConvert.DeserializeObject<Dictionary<string, LevelsStat>>(baseParameters.levels_stat_bonus);
        SaveFormattedJson(fileName, levelsStatDict);
        yield return null;
    }
    private static IEnumerator FillAlternatingBonusLevel(string fileName, BaseValuesFromFirebaseRemoteConfig baseValues)
    {
        var alternating = new AlternatingBonusLevel { alternatingBonusLevel = baseValues.alternatingBonusLevel };
        SaveJson(fileName, alternating);
        yield return null;
    }
    private static IEnumerator FillPlayerStats(string fileName, BaseValuesFromFirebaseRemoteConfig baseValues)
    {
        var playerStats = new PlayerStatsDefault
        {
            battery = baseValues.battery,
            fx = baseValues.fx,
            gas = baseValues.gas,
            hp = baseValues.hp,
            hp_regen = baseValues.hp_regen,
            oil = baseValues.oil,
            power = baseValues.power,
            steel = baseValues.steel,
            metal = baseValues.metal,
            xp = baseValues.xp,
            level_complete_reward = baseValues.level_complete_reward,
            show_blocker = baseValues.show_blocker,
            start_coins = baseValues.start_coins,
            test = baseValues.test
        };
        SaveJson(fileName, playerStats);
        yield return null;
    }
    private static IEnumerator FillWeaponsStats(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var weaponsStats = new WeaponCharacteristics { weapon_characteristics = baseParameters.weapons_characteristics };
        SaveJson(fileName, weaponsStats);
        yield return null;
    }
    private static IEnumerator FillSuppliesStats(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var suppliesStats = new SuppliesCharacteristics { supplies_characteristics = baseParameters.supplies_characteristics };
        SaveJson(fileName, suppliesStats);
        yield return null;
    }
    private static IEnumerator FillWarHouseStats(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var warHouseStats = new WarhouseCharacteristics { warhouse_characteristics = baseParameters.warhouse_characteristics };
        SaveJson(fileName, warHouseStats);
        yield return null;
    }
    private static IEnumerator FillDefaultItemsInSlots(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var slots = new DefaultItemsInSlots { default_items_in_slots = baseParameters.default_items_in_slots };
        SaveJson(fileName, slots);
        yield return null;
    }
    private static IEnumerator FillDefaultItemsInInventory(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var itemsInventory = new DefaultItemsInInventory { default_items_in_inventory = baseParameters.default_items_in_inventory };
        SaveJson(fileName, itemsInventory);
        yield return null;
    }
    private static IEnumerator FillDefaultItemsInGarage(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var itemsInventory = new DefaultItemsInGarage { default_items_in_garage = baseParameters.default_items_in_garage };
        SaveJson(fileName, itemsInventory);
        yield return null;
    }
    private static IEnumerator FillLdc(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var itemsInventory = new Ldc { ldc = baseParameters.ldc };
        SaveJson(fileName, itemsInventory);
        yield return null;
    }
    private static IEnumerator FillLdcb(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var itemsInventory = new Ldc { ldc = baseParameters.ldcb };
        SaveJson(fileName, itemsInventory);
        yield return null;
    }
    private static IEnumerator FillBaseDefaultStat(string fileName, string baseDefaultStatJson)
    {
        var baseDefaultStat = JsonConvert.DeserializeObject<BaseDefaultStat>(baseDefaultStatJson);
        SaveFormattedJson(fileName, baseDefaultStat);
        yield return null;
    }
    private static IEnumerator FillWeaponsUpgradeFormulas(string fileName, BaseValuesFromFirebaseRemoteConfig baseParameters)
    {
        var weaponsUpgradeFormulas = JsonConvert.DeserializeObject<WeaponsUpgradeFormulas>(baseParameters.weapons_upgrade_formulas);
        SaveFormattedJson(fileName, weaponsUpgradeFormulas);
        yield return null;
    }
    #endregion
}
