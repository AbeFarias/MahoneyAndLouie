using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HitPause hitPause;
    public int health = 100;
    private bool dead;
    
    private Animator _animator;
    private BoxCollider2D _collider2D;

    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Dead = Animator.StringToHash("Dead");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            _animator.SetBool(Dead, true);
        }
        else
        {
            _animator.SetTrigger(Idle);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        _animator.SetTrigger(Damage);
    }

    #region AnimatorEventHandlers
    public void Enemy_Damage()
    {
        CameraManager.CameraInstance.ShakeCamera(0.2f, 0.1f);
        hitPause.Freeze();
        if (health <= 0)
        {
            Die();
        }
    }
    #endregion

    private void Die()
    {
        dead = true;
        Destroy(_collider2D);
    }
}
