using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthUnits = 16f;

    void Start()
    {
        
    }

    void Update()
    {
        float mousePos = (Input.mousePosition.x / Screen.width * screenWidthUnits); //in units
        Vector2 paddlePos = new Vector2(mousePos, transform.position.y);
        transform.position = paddlePos;
    }
}
