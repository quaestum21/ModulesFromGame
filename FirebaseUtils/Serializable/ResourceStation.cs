using System.Collections.Generic;

[System.Serializable]
public class ResourceStation
{
    public string IndGen;
    public string IndPerHour;
    public string IndEfficienty;
    public string IndGoldCost;
    public string IndStellCost;
    public string PerHourBase;
    public string EfficientyBase;
    public List<string> CostBaseCapacity;
    public List<string> CostBasePerHour;
    public List<string> CostBaseEfficient;
}
