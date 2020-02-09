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

    [Serializable]
    public struct AnimInfo
    {
        public string animatorOwner;
        public Animator anim;
    }

    public ClipInfo[] info;
    public AnimInfo[] anims;

    private Dictionary<string, AnimationEvent> events = new Dictionary<string, AnimationEvent>();
    private Dictionary<string, AnimationClip> clips = new Dictionary<string, AnimationClip>();
    private Dictionary<string, Animator> animOwners = new Dictionary<string, Animator>();
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

        foreach (var data in anims.ToList())
        {
            animOwners.Add(data.animatorOwner, data.anim);
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
                var animatorController = animOwners["Player"].runtimeAnimatorController.animationClips[0];
                events[key].time = animatorController.length * 0.5f;
                events[key].functionName = "Attack_Middle";
                clips[key] = animatorController;
                clips[key].AddEvent(events[key]);
                break;
            
            case("Attack_End"):
                var runtimeAnimatorController = animOwners["Player"].runtimeAnimatorController.animationClips[0];
                events[key].time = runtimeAnimatorController.length * 0.9f;
                events[key].functionName = "Attack_End";
                clips[key] = runtimeAnimatorController;
                clips[key].AddEvent(events[key]);
                break;
            
            case("Enemy_Hitstun"):
                var enemyAnimatorController = animOwners["Enemy"].runtimeAnimatorController.animationClips[2];
                events[key].time = enemyAnimatorController.length * 0.5f;
                events[key].functionName = "Enemy_Damage";
                clips[key] = enemyAnimatorController;
                clips[key].AddEvent(events[key]);
                break;
        }
    }
}
