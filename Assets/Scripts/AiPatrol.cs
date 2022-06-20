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
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject Proyectile;
    private bool mustFlip;
    private bool Attacking;

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
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
                chaseSpeed *= -1;
            }
            
            mustPatrol = false;
            rigidBody.velocity = Vector2.zero;
            StartCoroutine(Attack());
        }else
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
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x*-1,transform.localScale.y);
        enemySpeed *= -1;
        mustPatrol = true;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        Attacking = true;
        rigidBody.velocity = new Vector2(chaseSpeed*Time.fixedDeltaTime, rigidBody.velocity.y);
        if(mustFlip || bodyCollider.IsTouchingLayers(wallLayer))
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
}
