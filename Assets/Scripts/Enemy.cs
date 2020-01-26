using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    private bool dead;
    
    private Animator _animator;

    private BoxCollider2D _collider2D;
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
            _animator.SetBool("Dead", true);
        }
        else
        {
            _animator.SetTrigger("Idle");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        _animator.SetTrigger("Damage");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Dead");
        dead = true;
        Destroy(_collider2D);
    }
}
