using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthUnits = 16f;
    [SerializeField] private float minX = 1f;
    [SerializeField] private float maxX = 15f;

    private void Start()
    {

    }

    private void Update()
    {
        float mousePos = (Input.mousePosition.x / Screen.width * screenWidthUnits); //in units
        Vector2 paddlePos = new Vector2(mousePos, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePos, minX, maxX);
        transform.position = paddlePos;
    }
}
