using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossScript : MonoBehaviour
{
    [SerializeField] private bool mustPatrol;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float enemySpeed, range, timeBetweenAttack, chaseSpeed,shootSpeed, timeBetweenShoot;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform player, shootPos, fireWallPos1, fireWallPos2;
    [SerializeField] private GameObject projectile, FireWall, projectileFall;
    [SerializeField] private AudioSource walkSoundEffect;
    [SerializeField] private Transform platform1,platform2,platform3;
    [SerializeField] private GameObject projectileEffect;
    [SerializeField] private int hp;
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
        flipOff = false;
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
            if(player.position.x > transform.position.x || player.position.x < transform.position.x )
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
            if(!Attacking)
            {
                StartCoroutine(Attack());
            }
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
    }

    void Flip()
    {
        if(!flipOff)
        {
            mustPatrol = false;
            transform.Rotate(0f,180f,0f);
            enemySpeed *= -1;
            chaseSpeed *= -1;
            mustPatrol = true;
            //walkSoundEffect.Play();
        }
        
    }

    IEnumerator Attack()
    {
        Attacking = true;
        
        yield return new WaitForSeconds(timeBetweenAttack);
        if(!flipOff)
        {
            rigidBody.velocity = new Vector2(enemySpeed*Time.fixedDeltaTime, rigidBody.velocity.y);
            int randomNumber =  Random.Range(0, 4);
            for(int i = 0; i< randomNumber;i++)
            {
                yield return new WaitForSeconds(timeBetweenShoot);
                StartCoroutine(Shoot());
            }
            int randomNumber2 =  1;
            if(randomNumber2 == 1 && hp<6)
            {
                yield return new WaitForSeconds(timeBetweenShoot*2);
                GameObject newFireWall1 = Instantiate(FireWall,fireWallPos2.position,Quaternion.identity);
                GameObject newFireWall2 = Instantiate(FireWall,fireWallPos1.position,Quaternion.identity);  
                StartCoroutine(FireFall());
                yield return new WaitForSeconds(timeBetweenShoot+1+timeBetweenShoot);
                Destroy(newFireWall1);
                Destroy(newFireWall2);

            }

        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }    
        Attacking = false;
        
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeBetweenShoot);
        rigidBody.velocity = Vector2.zero;
        GameObject newProjectile = Instantiate(projectile,shootPos.position,Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed *chaseSpeed * Time.fixedDeltaTime,0f);
        newProjectile.transform.localScale = new Vector2(newProjectile.transform.localScale.x*chaseSpeed, newProjectile.transform.localScale.y );
    }

    IEnumerator FireFall()
    {
        rigidBody.velocity = Vector2.zero;
        Instantiate(projectileEffect, platform1.position, Quaternion.identity);
        Instantiate(projectileEffect, platform2.position, Quaternion.identity);
        Instantiate(projectileEffect, platform3.position, Quaternion.identity);

        yield return new WaitForSeconds(timeBetweenShoot+1);

        GameObject fireFall1 = Instantiate(projectileFall,platform1.position,Quaternion.Euler (0f, 0f, -90f));
        GameObject fireFall2 = Instantiate(projectileFall,platform2.position,Quaternion.Euler (0f, 0f, -90f));
        GameObject fireFall3 = Instantiate(projectileFall,platform3.position,Quaternion.Euler (0f, 0f, -90f));
        
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
            hp--;
            if(hp == 0)
            {
                Destroy(gameObject);
            }
        }    
    }
}
