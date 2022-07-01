using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrol : MonoBehaviour
{

    [SerializeField] private bool mustPatrol;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float enemySpeed, range, timeBetweenAttack, chaseSpeed;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject Proyectile;
    [SerializeField] private AudioSource walkSoundEffect;   
    private float distanceToPlayer;
    private float xCollision;
    private bool flipOff;
    private Animator animator;
    private bool mustFlip;
    private bool Attacking;

    void OnEnable()
    {
        this.animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        Attacking = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if(distanceToPlayer <= range)
        {
            StartAttack();
        }
        else
        {
            mustPatrol = true;
        }
    }

    void FixedUpdate()
    {
        if(mustPatrol || Attacking)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f,groundLayer);
        }
    }

    void Patrol()
    {
        if(mustFlip || bodyCollider.IsTouchingLayers(wallLayer))
        {
            Flip();
        }
        rigidBody.velocity = new Vector2(enemySpeed*Time.fixedDeltaTime, rigidBody.velocity.y);
    }

    void Flip()
    {
        if(!flipOff)
        {
            mustPatrol = false;
            transform.localScale = new Vector2(transform.localScale.x*-1,transform.localScale.y);
            enemySpeed *= -1;
            chaseSpeed *= -1;
            mustPatrol = true;
            walkSoundEffect.Play();
        }
        
    }

    void StartAttack()
    {
        if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            if(!flipOff)
            {
                if(player.position.x > transform.position.x && enemySpeed < 0)
                {   
                    Flip();
                }
                else if(player.position.x < transform.position.x && enemySpeed > 0)
                {
                    Flip();
                }
            }
        }
        
        mustPatrol = false;
        mustFlip=false;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        if(!flipOff)
        {
            Attacking = true;
            rigidBody.velocity = new Vector2(chaseSpeed*Time.fixedDeltaTime, rigidBody.velocity.y);
            if((!Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f,groundLayer)) || bodyCollider.IsTouchingLayers(wallLayer))
            {
                rigidBody.velocity = Vector2.zero;
                
            }
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            flipOff = true;
            xCollision = player.position.x;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            flipOff = false;
            if(player.position.x > xCollision && enemySpeed < 0)
            {
                Flip();
            }
            else if(player.position.x < xCollision && enemySpeed > 0)
            {
                Flip();
            }

        }        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        } 
        if(other.gameObject.CompareTag("Enemy"))
        {
            Flip();
        }   
    }
}
