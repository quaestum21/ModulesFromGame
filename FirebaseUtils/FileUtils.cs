using System;
using System.IO;
using UnityEngine;

public static class FileUtils
{
    public static void CreateFile(string fileName)
    {
        try
        {
            File.WriteAllText(fileName, "");
            Debug.Log("Created new JSON file: " + fileName);
        }
        catch (Exception e)
        {
            Debug.LogError("Error creating JSON file: " + e.Message);
        }
    }
}
