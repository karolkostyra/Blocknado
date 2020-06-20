using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer block;
    private Grid grid;

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
        //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
        GameObject.Instantiate(block).transform.position = finalPosition;
        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = nearPoint;
    }
}