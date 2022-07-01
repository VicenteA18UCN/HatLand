using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinManager skinManager;
    [SerializeField]private float playerVelocity;
    [SerializeField]private float jumpForce;
    private Animator animator;
    [SerializeField] private bool isDead;
    public bool saveGame;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D playerRigidBody;
    public static bool canGlide;
    [SerializeField] private float glideEffect;
    private float glideEffect2;
    private bool powerFeather;
    [SerializeField] private RuntimeAnimatorController[] animators;
    public Feet feet;
    private bool direction;
    private int directionShoot;
    private bool newDirection;
    [SerializeField] private float shootSpeed, shootTime;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject projectile;
    private bool isShooting;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource walkSoundEffect;
    [SerializeField] private AudioSource landingSoundEffect;
    [SerializeField] private AudioSource fireBallgSoundEffect;
    private bool isMoving;
    private bool playOnce;
    [SerializeField] GameObject textKey;

    private void OnEnable()
    {
        this.PlayerComponents();
    }
    // Start is called before the first frame update
    void Start()
    {
        canGlide = false;
        this.SkinSelector();

    }

    // Update is called one per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            BossScript.hp--;
        }
        if (BossScript.hp==0)
        {
            StartCoroutine(Cinematic());
            BossScript.hp = 10;
        }
        this.PlayerAnimations();
        this.PlayerJump();
        PlayerShoot();
        WalkSoundEffect();
    }

    void FixedUpdate()
    {
        this.PlayerMovements();
    }

    void PlayerComponents()
    {
        this.glideEffect2 = 1;
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.playerRigidBody = GetComponent<Rigidbody2D>();
        this.direction = true;
        this.newDirection = true;
        this.directionShoot = 1;
        this.isMoving = false;
        this.playOnce = true;
    }

    IEnumerator Cinematic()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("FinalCinematic");

    }
    void PlayerMovements()
    {
        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            //playerRigidBody.velocity = new Vector2(200*Time.fixedDeltaTime, playerRigidBody.velocity.y);
            transform.Translate(0,0,0);
            transform.Translate(playerVelocity,0,0);
            //spriteRenderer.flipX = false;
            newDirection =  true;
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //playerRigidBody.velocity = new Vector2(-200*Time.fixedDeltaTime, playerRigidBody.velocity.y);
            transform.Translate(playerVelocity,0,0);
            //spriteRenderer.flipX = true;
            newDirection = false;
        }
        if(!(direction == newDirection))
        {
            transform.Rotate(0f,180f,0f);
            directionShoot = directionShoot*-1;
            direction = newDirection;
        }
    }

    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !feet.isJumping)
        {
            isMoving = false;
            jumpSoundEffect.Play();
            playerRigidBody.velocity = new Vector2(0,0);
            playerRigidBody.AddForce(new Vector3 (0,jumpForce*glideEffect2,0),ForceMode2D.Impulse);
        }
    }
    void PlayerAnimations()
    {
        if(feet.isJumping)
        {
            animator.SetBool("isWalk",false);
            return;
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("isWalk",true);
            isMoving = true;
        }
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalk",false);
            isMoving = false;
        }
    }

    public void StopPowerFeather()
    {
        this.glideEffect2 = 1;
        this.powerFeather = false;
    }

    public void StartPowerFeather()
    {
        this.glideEffect2 = glideEffect;
        this.powerFeather = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            isMoving=true;
            canGlide = false;
            animator.SetBool("isJump",false);
            animator.SetBool("isWalk",true);
            landingSoundEffect.Play();
        }
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {        
            isMoving=false;
            canGlide = false;
            animator.SetBool("isJump",false);
            animator.SetBool("isWalk",true);
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            deathSoundEffect.Play();
            playerRigidBody.velocity = new Vector2(0,0);
            this.isDead=true;
        }

        if(other.gameObject.CompareTag("NextLevel"))
        {
            saveGame = true;
            LevelManager.LoadNextNivel();
        }
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            saveGame = true;
        }
        if(other.gameObject.CompareTag("MovingOneWayPlatform"))
        {
            isMoving=false;
            canGlide = false;
            animator.SetBool("isJump",false);
            animator.SetBool("isWalk",true);
        }
        if(other.gameObject.CompareTag("FinalLevel"))
        {
            if(GameManager.hasKey)
            {
                saveGame = true;
                LevelManager.LoadNextNivel();
            }
            else
            {
                textKey.SetActive(true);
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            animator.SetBool("isJump",true);
            isMoving = false;
        }
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {            
            animator.SetBool("isJump",true);
            isMoving = false;
        }
        if(other.gameObject.CompareTag("MovingOneWayPlatform"))
        {
            animator.SetBool("isJump",true);
            isMoving = false;
            playerRigidBody.velocity = new Vector2(0,playerRigidBody.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("MovingOneWayPlatform"))
        {
            isMoving=false;
            canGlide = false;
            animator.SetBool("isJump",false);
            animator.SetBool("isWalk",true);
            if(!Input.GetKeyDown(KeyCode.D) || !Input.GetKeyDown(KeyCode.A)|| !Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidBody.velocity = new Vector2(other.gameObject.GetComponent<HorizontalMovingPlatform>().GetVelocity().x,playerRigidBody.velocity.y);
            }
            else
            {
                playerRigidBody.velocity = new Vector2(0,0);
            }

            
        }
    }    


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            saveGame = true;
        }
    }



    
    public bool GetPlayerStatus()
    {
        return this.isDead;
    }

    public void ResetPlayerStatus()
    {
        this.isDead = false;
    }

    void PlayerShoot()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        fireBallgSoundEffect.Play();
        GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed*directionShoot*Time.fixedDeltaTime,0f);
        newProjectile.transform.localScale = new Vector2(newProjectile.transform.localScale.x*directionShoot, newProjectile.transform.localScale.y );
        //Animacion de disparo si tenemos

        yield return new WaitForSeconds(shootTime);
        isShooting = false;

    }

    void WalkSoundEffect()
    {
        if(isMoving)
        {
            if(playOnce)
            {
                playOnce = false;
                walkSoundEffect.Play();
            }
        }
        else
        {
            playOnce = true;
            walkSoundEffect.Stop();
        }
    }
    public bool GetSaveGame(){return this.saveGame;}

    public void SetSaveGame(bool saveGame){this.saveGame = saveGame;}

    public void SkinSelector()
    {
        GetComponent<SpriteRenderer>().sprite = skinManager.GetSelectedSkin().sprite;
        if (skinManager.GetSkinIndex() == 0)
        {
            animator.runtimeAnimatorController = animators[0];
        }
        if (skinManager.GetSkinIndex() == 1)
        {
            animator.runtimeAnimatorController = animators[1];
    }
    }
}
