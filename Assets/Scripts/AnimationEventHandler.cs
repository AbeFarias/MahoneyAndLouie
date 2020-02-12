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
            // Use this case for damaging enemy as Louie
            case ("Louie_Attack_Middle"):
                var louieAttackMiddleAnim = animOwners["Louie"].runtimeAnimatorController.animationClips[0];
                events[key].time = louieAttackMiddleAnim.length * 0.5f;
                events[key].functionName = "Louie_Attack_Middle";
                clips[key] = louieAttackMiddleAnim;
                clips[key].AddEvent(events[key]);
                break;
            // Use this case for resetting Louie's position after attack
            case("Louie_Attack_End"):
                var louieAttackEnd = animOwners["Louie"].runtimeAnimatorController.animationClips[0];
                events[key].time = louieAttackEnd.length * 0.9f;
                events[key].functionName = "Louie_Attack_End";
                clips[key] = louieAttackEnd;
                clips[key].AddEvent(events[key]);
                break;
            // Use this case for PARRYING enemies as Mahoney
            case ("Mahoney_Attack_Middle"):
                var mahoneyAttackMiddle = animOwners["Mahoney"].runtimeAnimatorController.animationClips[0];
                events[key].time = mahoneyAttackMiddle.length * 0.5f;
                events[key].functionName = "Mahoney_Attack_Middle";
                clips[key] = mahoneyAttackMiddle;
                clips[key].AddEvent(events[key]);
                break;
            // Use this case for resetting animation for Mahoney after parry animation
            case("Mahoney_Attack_End"):
                var mahoneyAttackEnd = animOwners["Mahoney"].runtimeAnimatorController.animationClips[0];
                events[key].time = mahoneyAttackEnd.length * 0.9f;
                events[key].functionName = "Mahoney_Attack_End";
                clips[key] = mahoneyAttackEnd;
                clips[key].AddEvent(events[key]);
                break;
            
            // Use this case hitting enemy
            case("Enemy_Hitstun"):
                var enemyHitStunAnim = animOwners["Enemy"].runtimeAnimatorController.animationClips[2];
                events[key].time = enemyHitStunAnim.length * 0.5f;
                events[key].functionName = "Enemy_Damage";
                clips[key] = enemyHitStunAnim;
                clips[key].AddEvent(events[key]);
                break;
            
            // Mainly used for creating the parry window
            case("Enemy_Attack_Begin"):
                var enemyAttackBegin = animOwners["Enemy"].runtimeAnimatorController.animationClips[3];
                events[key].time = enemyAttackBegin.length * 0.1f;
                events[key].functionName = "Enemy_Attack_Begin";
                clips[key] = enemyAttackBegin;
                clips[key].AddEvent(events[key]);
                break;
            
            // Mainly used for closing the parry window and attack reg
            case("Enemy_Attack_Middle"):
                var enemyAttackAnim = animOwners["Enemy"].runtimeAnimatorController.animationClips[3];
                events[key].time = enemyAttackAnim.length * 0.6f;
                events[key].functionName = "Enemy_Attack";
                clips[key] = enemyAttackAnim;
                clips[key].AddEvent(events[key]);
                break;
            
            // Use this to reset enemy animation after attack
            case("Enemy_Attack_End"):
                var enemyAttackEndAnim = animOwners["Enemy"].runtimeAnimatorController.animationClips[1];
                events[key].time = enemyAttackEndAnim.length * 0.9f;
                events[key].functionName = "Enemy_Attack_End";
                clips[key] = enemyAttackEndAnim;
                clips[key].AddEvent(events[key]);
                break;
        }
    }
}
