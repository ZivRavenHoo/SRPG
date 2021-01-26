using System;
using System.IO;
using LitJson;
using UnityEditor;

public static class FileOperation
{
    public static void ObjectToJsonFile(string path,Object data)
    {
        string json = JsonMapper.ToJson(data);
        FileInfo file = new FileInfo(path);
        StreamWriter sw = file.CreateText();
        sw.Write(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }

    public static T JsonFileToObject<T>(string path)
    {
        StreamReader sr = new StreamReader(path);
        string json = sr.ReadLine();
        return JsonMapper.ToObject<T>(json);
    }

    public static string GetMapDataPath(string mapName)
    {
        return UnityEngine.Application.dataPath + GameConstant.MapDataFilePath + mapName + ".json";
    }
}
