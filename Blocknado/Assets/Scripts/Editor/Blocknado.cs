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

        if (GUILayout.Button("Instantiate + set positions from loaded data", editorButton, GUILayout.Height(30)))
        {
            InstantiateBlocks();
        }
        if (GUILayout.Button("SaveArray", editorButton, GUILayout.Height(30)))
        {
            Save();
        }
        if (GUILayout.Button("Load and debug.log", editorButton, GUILayout.Height(30)))
        {
            Load();
        }
    }

    private void InstantiateBlocks()
    {
        string path = "Assets/Prefabs";
        string prefabName = "Block";
        GameObject parent = GameObject.FindGameObjectWithTag("Blocks");

        blockPlacer = FindObjectOfType<BlockPlacer>();
        var list = blockPlacer.LoadArray();

        for (int i = 0; i < list.Count; i=i+2)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(path + "/" + prefabName
                + ".prefab", typeof(GameObject));
            if (prefab != null)
            {
                GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                obj.transform.SetParent(parent.transform);
                obj.transform.position = new Vector2(list[i], list[i + 1]);
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

        var list = blockPlacer.LoadArray();

        for (int i = 0; i < list.Count; i=i+2)
        {
            Debug.Log("x: " + list[i] + ", y: " + list[i + 1]);
        }
    }
}