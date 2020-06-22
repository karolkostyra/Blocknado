using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private BlocksData _BlockData = new BlocksData();
    private Grid grid;
    public List<GameObject> list = new List<GameObject>();

    [System.Serializable]
    public class BlocksData
    {
        public List<float> blocksList = new List<float>();
    }


    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlaceBlockNear(ray);
        }
    }

    private void PlaceBlockNear(Vector3 clickPoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject obj = Instantiate(block);
        obj.transform.position = finalPosition;
        list.Add(obj);
    }

    public void SaveArray()
    {
        SaveDataBlocks();
        string block = JsonUtility.ToJson(_BlockData);
        Debug.Log(block);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/BlocksData.json", block);
    }

    public List<float> LoadArray()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "BlocksData.json");
        

        if (File.Exists(filePath))
        {
            string data = System.IO.File.ReadAllText(filePath);
            BlocksData blocksData = JsonUtility.FromJson<BlocksData>(data);
            //Debug.Log(blocksData.blocksList);
            return blocksData.blocksList;
            
        }
        else
        {
            Debug.Log("ERROR - FILE NOT FOUND!");
            return null;
        }
    }

    private void SaveDataBlocks()
    {
        for (int i = 0; i < list.Count; i++)
        {
            _BlockData.blocksList.Add(list[i].transform.position.x);
            _BlockData.blocksList.Add(list[i].transform.position.y);
        }
    }
}