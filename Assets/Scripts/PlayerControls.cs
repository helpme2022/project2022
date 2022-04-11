using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls: MonoBehaviour
{
    [Header("Main Values")]
    public float MovementSpeed;
    private Rigidbody2D _rigidbody;

    [Header("Jump & Land")]
    private bool isGrounded;
    public float JumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    [Header("Basic Sprites Managment Values")]
    public Animator animator;
    private bool facingRight = true;

    [Header("Attacking Values")]
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rigidbody.velocity = Vector2.up * JumpForce;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        var movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (movement < 0 && facingRight)
        {
            Flip();
        }
        if (movement > 0 && !facingRight)
        {
            Flip();
        }

    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
