using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject smallBlock;
    [SerializeField] private GameObject horizontalBlock;
    public bool smallBlocks = false;
    
    private Grid grid;
    private Vector3 ray;

    [SerializeField] private BlocksData _BlockData = new BlocksData();
    public List<GameObject> list = new List<GameObject>();

    [System.Serializable]
    public class BlocksData
    {
        public List<string> blockNames = new List<string>();
        public List<float> blocksList = new List<float>();
    }


    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        UpdateBlockList();
    }

    private void Update()
    {
        ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var leftCtrl = Input.GetKey(KeyCode.LeftControl);

        if (!leftCtrl && Input.GetMouseButtonDown(0))
        {
            PlaceBlockNear(ray, block);
        }
        else if (leftCtrl && Input.GetMouseButtonDown(0))
        {
            PlaceBlockNear(ray, horizontalBlock);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            PlaceBlockNear(ray, smallBlock);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            RemoveBlockNear();
        } 
    }

    private void PlaceBlockNear(Vector3 clickPoint, GameObject prefab)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject obj = Instantiate(prefab);
        obj.transform.position = finalPosition;
        obj.tag = "Block";
        list.Add(obj);
    }

    private void RemoveBlockNear()
    {
        var targetTag = "Block";
        Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                     Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);

        if(hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            Vector3 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var finalPosition = grid.GetNearestPointOnGrid(ray);


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].transform.position == finalPosition)
                {
                    Destroy(list[i]);
                    list.Remove(list[i]);
                }
            }
        }
    }

    private void UpdateBlockList()
    {
        var parent = GameObject.FindGameObjectWithTag("Blocks");

        foreach (Transform child in parent.transform)
        {
            list.Add(child.gameObject);
        }
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
            BlocksData blocksData = JsonUtility.FromJson<BlocksData>(data);;
            return blocksData.blocksList;
            
        }
        else
        {
            Debug.Log("ERROR - FILE NOT FOUND!");
            return null;
        }
    }

    public List<string> LoadNames()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "BlocksData.json");

        if (File.Exists(filePath))
        {
            string data = System.IO.File.ReadAllText(filePath);
            BlocksData blocksData = JsonUtility.FromJson<BlocksData>(data); ;
            return blocksData.blockNames;

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
            _BlockData.blockNames.Add(list[i].name);
        }
    }
}