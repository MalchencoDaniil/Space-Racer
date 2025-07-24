using UnityEngine;
using System.Collections.Generic;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private List<Transform> cameraPoints = new List<Transform>();

    public void MoveCamera(int pointIndex)
    {
        mainCamera.transform.position = cameraPoints[pointIndex].position;
    }
}