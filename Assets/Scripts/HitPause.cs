using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    public float duration = 0.25f;

    private float _leftOverFreezeDuration;
    private bool _isFrozen;
    
    // Update is called once per frame
    void Update()
    {
        if (_leftOverFreezeDuration > 0 && !_isFrozen) StartCoroutine(HandleFreeze());
    }

    public void Freeze()
    {
        _leftOverFreezeDuration = duration;
    }

    IEnumerator HandleFreeze()
    {
        _isFrozen = true;
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        _leftOverFreezeDuration = 0f;
        _isFrozen = false;
    }
}
