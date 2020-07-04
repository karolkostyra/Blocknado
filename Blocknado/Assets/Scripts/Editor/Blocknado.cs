using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

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
        if (GUILayout.Button("Remove the blocks", editorButton, GUILayout.Height(30)))
        {
            RemoveBlocks();
        }
        if (GUILayout.Button("Create and prepare new level", editorButton, GUILayout.Height(30)))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            CreateNewLevel();
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
        //string prefabName = "Block";
        GameObject parent = GameObject.FindGameObjectWithTag("Blocks");

        blockPlacer = FindObjectOfType<BlockPlacer>();
        var list = blockPlacer.LoadArray();
        var names = blockPlacer.LoadNames();

        for (int i = 0, j = 0; i < list.Count; i=i+2, j++)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(path + "/"
                + names[j].Substring(0,names[j].Length-7)
                + ".prefab", typeof(GameObject));
            if (prefab != null)
            {
                GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                obj.transform.SetParent(parent.transform);
                obj.transform.position = new Vector2(list[i], list[i + 1]);
            }
        }
    }

    private void RemoveBlocks()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Blocks");

        List<GameObject> list = new List<GameObject>();

        foreach(Transform child in parent.transform)
        {
            list.Add(child.gameObject);
        }

        for (int i = 0; i < list.Count; i++)
        {
            DestroyImmediate(list[i], false);
        }
    }

    private void CreateNewLevel()
    {
        string newLevelPath = "Assets/Scenes/Levels/NewLevel.unity";
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        LoadScenePrefabs();
        SaveCurrentScene(newLevelPath);
    }

    private void LoadScenePrefabs()
    {
        string path = "Assets/Prefabs";
        List<string> prefabNames = new List<string>() {"Main Camera", "Ball", "Paddle", "GameSpace",
                                    "Grid", "BlockPlacer", "Blocks", "EventSystem", "Level", "GameStatus"};

        for (int i = 0; i < prefabNames.Count; i++)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(path + "/" + prefabNames[i]
                + ".prefab", typeof(GameObject));
            if(prefab != null)
            {
                PrefabUtility.InstantiatePrefab(prefab);
            }
        }
    }

    private void SaveCurrentScene(string scenePath)
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), scenePath);
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