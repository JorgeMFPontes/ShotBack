using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private GameObject cameraPosition;
    private Transform cameraPositions;

    private void Awake()
    {
        cameraPosition = GameObject.Find("CameraPos");
        cameraPositions = cameraPosition.GetComponent<Transform>();
    }
    private void Update()
    {
        transform.position = cameraPositions.position;
    }
}
