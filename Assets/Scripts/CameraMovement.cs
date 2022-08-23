using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private Transform target;
    
    [SerializeField] private float dampTime = 0.25f;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void Update () 
    {
        if (target)
        {
            var camPos = target.position;
            var point = _mainCamera.WorldToViewportPoint(camPos);
            var delta = camPos - _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            
            var camPosOnViewport = transform.position;
            var destination = camPosOnViewport + delta;
            
            camPosOnViewport = Vector3.SmoothDamp(camPosOnViewport, destination, ref _velocity, dampTime);
            transform.position = camPosOnViewport;
        }
     
    }
}
