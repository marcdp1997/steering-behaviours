using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        mainCamera.transform.position = new Vector3(target.position.x, mainCamera.transform.position.y, target.position.z);
    }
}
