using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mahoney : MonoBehaviour, IPlayer
{
    public static Mahoney MahoneyInstance { get; private set; }
    public Animator Animator { get; set; }
    public PlayerInput Input { get; set; }
    public Rigidbody2D RB2D { get; set; }
    public bool InControl { get; set; }
    
    public int AttackDamage;
    public float MoveSpeed;
    public float AttackRange;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;

    private bool _inAttack;
    
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    private void Awake()
    {
        if (MahoneyInstance == null)
        {
            MahoneyInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        RB2D = GetComponent<Rigidbody2D>();
        Input = PlayerInput.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (InControl)
        {
            Animator.SetTrigger(Idle);
            RB2D.velocity = Vector2.zero;
            return;
        }

        if (_inAttack)
        {
            RB2D.velocity = Vector2.zero;
            return;
        }

        if (Input.Attack.IsPressed)
        {
            Attack();
            return;
        }
        
        if (Input.Left.IsPressed)
        {
            Animator.SetTrigger(Run);
            transform.localScale = new Vector2(1, 1);
            RB2D.velocity = Vector2.left * MoveSpeed;
        } 
        else if (Input.Right.IsPressed)
        {
            Animator.SetTrigger(Run);
            transform.localScale = new Vector2(-1, 1);
            RB2D.velocity = Vector2.right * MoveSpeed;
        }
        else
        {
            RB2D.velocity = Vector2.zero;
            Animator.SetTrigger(Idle);
        }
    }
    
    #region AnimationRecieverCalls
    public void Mahoney_Attack_Middle()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        foreach (var enemy in hitEnemies)
        {
            Debug.Log(enemy.name);
            if (enemy.GetComponent<Enemy>().AttackParryable)
            {
                Debug.Log("Triggered");
                enemy.GetComponent<Enemy>().TakeDamage(250);
            }
        }
    }

    public void Mahoney_Attack_End()
    {
        Animator.SetTrigger(Idle);
        _inAttack = false;
    }

    //TODO: Remove these two dummy calls once I have more animations
    public void Louie_Attack_Middle() { }
    public void Louie_Attack_End() { }
    #endregion

    public void Attack()
    {
        Animator.SetTrigger(Attack1);
    }

    public void TakeDamage()
    {
        Debug.Log("Mahoney took damage!");
    }
}
