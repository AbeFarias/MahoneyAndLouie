using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 _cameraOffset;
    private Transform currentPlayerTransform;
    [Range(0.01f, 1.0f)] public float Smooth = 0.5f;

    public static CameraManager CameraInstance { get; private set; }

    private void Awake()
    {
        if (CameraInstance == null)
        {
            CameraInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void ShakeCamera(float duration, float mag)
    {
        StartCoroutine(Shake(duration, mag));
    }
    
    public IEnumerator Shake(float duration, float mag)
    {
        Vector3 orig = transform.localPosition;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * mag;
            float y = UnityEngine.Random.Range(-1f, 1f) * mag;

            transform.localPosition = new Vector3(x, y, orig.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = orig;
    }
}
