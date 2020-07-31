using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
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
    // Start is called before the first frame update slurp
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
            // Use this case for damaging enemy as Louie
            case ("Louie_Attack_Middle"):
                AddToEvents(key, "Louie", 0, 0.5f, "Louie_Attack_Middle");
                break;
            // Use this case for resetting Louie's position after attack
            case("Louie_Attack_End"):
                AddToEvents(key, "Louie", 0, 0.9f, "Louie_Attack_End");
                break;
            // Use this case for PARRYING enemies as Mahoney
            case ("Mahoney_Attack_Middle"):
                AddToEvents(key, "Mahoney", 0, 0.5f, "Mahoney_Attack_Middle");
                break;
            // Use this case for resetting animation for Mahoney after parry animation
            case("Mahoney_Attack_End"):
                AddToEvents(key, "Mahoney", 0, 0.9f, "Mahoney_Attack_End");
                break;
            
            // Use this case hitting enemy
            case("Enemy_Hitstun"):
                AddToEvents(key, "Enemy", 2, 0.5f, "Enemy_Damage");
                break;
            
            // Mainly used for creating the parry window
            // TODO: Investigate bug where the index of this one doesn't matter.
            case("Enemy_Attack_Begin"):
                AddToEvents(key, "Enemy", 3, 0.2f, "Enemy_Attack_Begin");
                break;
            
            // Mainly used for closing the parry window and attack reg
            case("Enemy_Attack_Middle"):
                AddToEvents(key, "Enemy", 3, 0.5f, "Enemy_Attack");
                break;
            
            // Use this to reset enemy animation after attack
            case("Enemy_Attack_End"):
                AddToEvents(key, "Enemy", 3, 0.9f, "Enemy_Attack_End");
                break;
        }
    }

    private void AddToEvents(string key, string owner, int index, float triggerTime, string functionName)
    {
        var anim = animOwners[owner].runtimeAnimatorController.animationClips[index];
        events[key].time = anim.length * triggerTime;
        events[key].functionName = functionName;
        clips[key] = anim;
        clips[key].AddEvent(events[key]);
    }
}
