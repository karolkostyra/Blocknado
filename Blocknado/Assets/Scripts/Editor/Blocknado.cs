using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class Blocknado : EditorWindow
{
    [MenuItem("Window/Blocknado")]
    public static void ShowWindow()
    {
        GetWindow<Blocknado>("Blocknado");
    }
    
    private BlockPlacer blockPlacer;

    void OnGUI()
    {
        GUIStyle editorButton = new GUIStyle("Button");
        editorButton.fontSize = 16;
        editorButton.fontStyle = FontStyle.Bold;

        GUILayout.Label("Own functions in editor:", EditorStyles.boldLabel);

        if (GUILayout.Button("Select solid and clone it as GOAL", editorButton, GUILayout.Height(30)))
        {
            InstantiateBlocks();
        }
        if (GUILayout.Button("Instantiate", editorButton, GUILayout.Height(30)))
        {
            InstantiateBlocks2();
        }
        if (GUILayout.Button("SaveArray", editorButton, GUILayout.Height(30)))
        {
            Save();
        }
        if (GUILayout.Button("LoadArray", editorButton, GUILayout.Height(30)))
        {
            Load();
        }
    }

    private void InstantiateBlocks()
    {
        blockPlacer = FindObjectOfType<BlockPlacer>();
        for (int i = 0; i < blockPlacer.list.Count; i++)
        {
            Debug.Log("test?");
            PrefabUtility.InstantiatePrefab(blockPlacer.list[i]);
        }
    }

    private void InstantiateBlocks2()
    {
        string path = "Assets/Prefabs";
        List<string> prefabNames = new List<string>() { "Block" };

        for (int i = 0; i < prefabNames.Count; i++)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(path + "/" + prefabNames[i]
                + ".prefab", typeof(GameObject));
            if (prefab != null)
            {
                PrefabUtility.InstantiatePrefab(prefab);
            }
        }
    }

    private void Save()
    {
        blockPlacer = FindObjectOfType<BlockPlacer>();

        blockPlacer.SaveArray();
    }

    private void Load()
    {
        blockPlacer = FindObjectOfType<BlockPlacer>();

        blockPlacer.LoadArray();
    }
}