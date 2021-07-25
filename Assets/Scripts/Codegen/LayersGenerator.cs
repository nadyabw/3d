using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LayersGenerator : MonoBehaviour
{
    [InitializeOnLoadMethod]
    private static void Generate()
    {
        var folderPath = FolderPath();
        string path = $"{folderPath}/Layers.cs";
        string code = $@"public static class Layers 
{{              
{String.Join("\n",
        GetLayerNames()
               .Where(layerName => layerName != "")
               .Select(layerName => $"    const string {layerName.Replace(" ", "_").ToUpper()} = \"{layerName}\";")
    )}
}}";

        File.WriteAllText(path, code);
    }

    private static string[] GetLayerNames()
    {
        string[] layers = new string[32];

        for (int i = 0; i < 32; i++)
        {
            layers[i] = LayerMask.LayerToName(i);
        }

        return layers;
    }

    private static string FolderPath()
    {
        string path = $"{Application.dataPath}/Scripts/Codegen";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }
}
