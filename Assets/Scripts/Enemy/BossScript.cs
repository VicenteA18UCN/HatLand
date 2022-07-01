using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] public static int hp = 10;
    private float distanceToPlayer;
    private float xCollision;
    private bool flipOff;
    private Animator animator;
    private bool mustFlip;
    private bool Attacking;
    private bool isChasing;
    private bool isFireBall;
    private bool startShooting;
    private bool startFireWall;
    private bool startFireFall;
    private bool shooting;
    private bool onceIsChasing;

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
        shooting = false;
        onceIsChasing = false;
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
            BossAnimations();
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
        }
        
    }

    void StartAttack()
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
        if(!Attacking && !shooting)
        {
            print("attacking");
            StartCoroutine(Attack());
            
        }
    }

    IEnumerator Attack()
    {
        Attacking = true;
        isChasing = true;
        yield return new WaitForSeconds(timeBetweenAttack);
        if(!flipOff)
        {
            rigidBody.velocity = new Vector2(enemySpeed*Time.fixedDeltaTime, rigidBody.velocity.y);
            isChasing = false;
            startShooting = true;
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
        if(startShooting)
        {
            print("shooting");
            StartCoroutine(StartShoot());
        }           
        
    }

    IEnumerator StartShoot()
    {
        startShooting = false;
        shooting = true;
        int randomNumber =  Random.Range(0, 4);
        int i = 0;
        while(i! != randomNumber)
        {
            yield return new WaitForSeconds(timeBetweenShoot*2);
            rigidBody.velocity = Vector2.zero;
            GameObject newProjectile = Instantiate(projectile,shootPos.position,Quaternion.identity);
            newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed *chaseSpeed * Time.fixedDeltaTime,0f);
            newProjectile.transform.localScale = new Vector2(newProjectile.transform.localScale.x*chaseSpeed, newProjectile.transform.localScale.y );
           
            i++;
            print(i);
        }
        if(i == randomNumber)
        {
            startFireWall = true;
            startFireFall = true;
            shooting = false;
            if(hp>5)
            {                        
                Attacking = false;
            }
        }
        if(hp<6)
            {
                if(startFireWall)
                {
                    StartCoroutine(StartFireWall());
                }
                if(startFireFall)
                {
                    StartCoroutine(FireFall());
                }
            }
    }

    IEnumerator StartFireWall()
    {
        startFireWall = false;
        if(hp<6)
        {
            print("firewall");
            GameObject newFireWall1 = Instantiate(FireWall,fireWallPos2.position,Quaternion.identity);
            GameObject newFireWall2 = Instantiate(FireWall,fireWallPos1.position,Quaternion.identity);
            startFireWall = true;
            yield return new WaitForSeconds(timeBetweenShoot+2);
            Destroy(newFireWall1);
            Destroy(newFireWall2);

        }
    }
    IEnumerator FireFall()
    {
        print("firefall");
        startFireFall = false;
        rigidBody.velocity = Vector2.zero;
        Instantiate(projectileEffect, platform1.position, Quaternion.identity);
        Instantiate(projectileEffect, platform2.position, Quaternion.identity);
        Instantiate(projectileEffect, platform3.position, Quaternion.identity);

        yield return new WaitForSeconds(timeBetweenShoot+1);

        GameObject fireFall1 = Instantiate(projectileFall,platform1.position,Quaternion.Euler (0f, 0f, -90f));
        GameObject fireFall2 = Instantiate(projectileFall,platform2.position,Quaternion.Euler (0f, 0f, -90f));
        GameObject fireFall3 = Instantiate(projectileFall,platform3.position,Quaternion.Euler (0f, 0f, -90f));
        Attacking = false;
        
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

    void BossAnimations()
    {
        
        if((-1f < rigidBody.velocity.magnitude) && (rigidBody.velocity.magnitude < 1f))
        {
            animator.SetBool("isChasing",false);
            walkSoundEffect.Stop();
            isChasing = false;
        }
        else if(rigidBody.velocity != Vector2.zero && !isChasing)
        {
            animator.SetBool("isChasing",true);
            walkSoundEffect.Play();
            isChasing = true;
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

    void FinalCinematic()
    {
        SceneManager.LoadScene("FinalCinematic");
    }

    public static void MenusHp()
    {
        hp--;
    }
}
