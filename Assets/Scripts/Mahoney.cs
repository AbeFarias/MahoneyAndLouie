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
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Run = Animator.StringToHash("Run");

    private void Awake()
    {
        if (MahoneyInstance == null)
        {
            MahoneyInstance = this;
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
        };
        
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
        else if (Input.Attack.WasPressed)
        {
            Attack();
        }
        else
        {
            RB2D.velocity = Vector2.zero;
            Animator.SetTrigger(Idle);
        }
    }

    public void Attack()
    {
        Animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }
    }
}
