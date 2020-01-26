using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 _cameraOffset;
    private Transform currentPlayerTransform;
    [Range(0.01f, 1.0f)] public float Smooth = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        if (Louie.LouieInstance.InControl)
        {
            currentPlayerTransform = Louie.LouieInstance.transform;
        }
        else
        {
            currentPlayerTransform = Mahoney.MahoneyInstance.transform;
        }
        _cameraOffset = transform.position - currentPlayerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Louie.LouieInstance.InControl)
        {
            currentPlayerTransform = Louie.LouieInstance.transform;
        }
        else
        {
            currentPlayerTransform = Mahoney.MahoneyInstance.transform;
        }
        
        Vector3 newPos = currentPlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, Smooth);
    }
}
