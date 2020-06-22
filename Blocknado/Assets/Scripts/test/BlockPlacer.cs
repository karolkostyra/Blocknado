using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject block;
    private Grid grid;
    public List<GameObject> list = new List<GameObject>();

    [SerializeField] private BlocksData _BlockData = new BlocksData();

    [System.Serializable]
    public class BlocksData
    {
        public string blockName;
    }


    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Start()
    {
        if(list == null)
        {
            list = new List<GameObject>(0);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlaceBlockNear(ray);
            //SaveArray();
        }
    }

    private void PlaceBlockNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
        GameObject obj = Instantiate(block);
        obj.transform.position = finalPosition;
        list.Add(obj);
        //PrefabUtility.InstantiatePrefab(obj);
        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }

    public void SaveArray()
    {
        string block = JsonUtility.ToJson(_BlockData);
        Debug.Log(block);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/BlocksData.json", block);
    }

    public void LoadArray()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "BlocksData.json");
        

        if (File.Exists(filePath))
        {
            string data = System.IO.File.ReadAllText(filePath);
            BlocksData blocksData = JsonUtility.FromJson<BlocksData>(data);
            Debug.Log(blocksData.blockName);
        }
        else
        {
            Debug.Log("ERROR - FILE NOT FOUND!");
            return;
        }
    }
}