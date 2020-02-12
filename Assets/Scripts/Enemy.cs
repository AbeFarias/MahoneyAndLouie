using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HitPause hitPause;
    
    public int health = 100;
    public float AttackDamage;
    public float AttackRange;
    public bool AttackParryable;
    
    public Transform AttackPoint;
    
    private bool dead;
    
    private float nextAttack;
    private float attackCooldown = 1.0f;
    
    private Animator _animator;
    private BoxCollider2D _collider2D;

    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Attack = Animator.StringToHash("Attack");

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
        
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackCooldown;
            ParryableAttack();
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

    public void Enemy_Attack_Begin()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        AttackParryable = true;
    }

    public void Enemy_Attack()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        AttackParryable = false;
        
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange);
        foreach (var player in hitPlayer)
        {
            if (player.GetComponent<Louie>() != null)
            {
                Louie.LouieInstance.TakeDamage();
            }

            if (player.GetComponent<Mahoney>() != null)
            {
                Mahoney.MahoneyInstance.TakeDamage();
            }
        }
    }

    public void Enemy_Attack_End()
    {
        //throw new NotImplementedException("Nothing happening at end of enemy attack");
    }
    #endregion

    private void Die()
    {
        dead = true;
        Destroy(_collider2D);
    }

    private void ParryableAttack()
    {
        _animator.SetTrigger(Attack);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null) return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
