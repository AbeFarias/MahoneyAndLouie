using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Serializable]
    public struct ClipInfo
    {
        public string key;
        public AnimationClip clip;
    }

    public ClipInfo[] info;
    public Animator anim;

    private Dictionary<string, AnimationEvent> events = new Dictionary<string, AnimationEvent>();
    private Dictionary<string, AnimationClip> clips = new Dictionary<string, AnimationClip>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var data in info.ToList())
        {
            clips.Add(data.key, data.clip);
        }
        
        foreach(var data in info)
        {
            events.Add(data.key, new AnimationEvent());
        }
        
        foreach (var key in clips.Keys.ToList())
        {
            SetEventParameters(key);
        }
    }

    private void SetEventParameters(string key)
    {
        switch (key)
        {
            case ("Attack_Middle"):
                var animatorController = anim.runtimeAnimatorController.animationClips[0];
                events[key].time = animatorController.length * 0.5f;
                events[key].functionName = "Attack_Middle";
                clips[key] = animatorController;
                clips[key].AddEvent(events[key]);
                break;
            
            case("Attack_End"):
                var runtimeAnimatorController = anim.runtimeAnimatorController.animationClips[0];
                events[key].time = runtimeAnimatorController.length * 0.9f;
                events[key].functionName = "Attack_End";
                clips[key] = runtimeAnimatorController;
                clips[key].AddEvent(events[key]);
                break;
        }
    }
}
