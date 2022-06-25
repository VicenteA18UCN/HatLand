using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinManager skinManager;
    private PlayerShoot platerShoot;
    private float playerVelocity;
    private float jumpForce;
    private Animator animator;
    [SerializeField] private bool isDead;
    [SerializeField] private bool saveGame;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D playerRigidBody;
    public static bool canGlide;
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

    private void OnEnable()
    {
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.playerRigidBody = GetComponent<Rigidbody2D>();
        this.direction = true;
        this.newDirection = true;
        this.directionShoot = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.playerVelocity = 0.1f;
        this.jumpForce = 7.8f;
        canGlide = false;
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

    // Update is called one per frame
    private void Update()
    {
        this.PlayerAnimations();
        this.PlayerJump();
        this.Gliding();
        PlayerShoot();
    }

    void FixedUpdate()
    {
        this.PlayerMovements();
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
            playerRigidBody.velocity = new Vector2(0,0);
            playerRigidBody.AddForce(new Vector3 (0,jumpForce,0),ForceMode2D.Impulse);
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
        }
        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalk",false);
        }
    }

    void Gliding()
    {
        if(powerFeather){
            if(canGlide){ 
            if(Input.GetKey(KeyCode.Space) && feet.isJumping)
        {
            Vector2 force = Vector2.up * 0.385f;
            playerRigidBody.AddForceAtPosition(force, transform.position);
        }
        }
        }

    }

    void CanGlide()
    {
        canGlide = true;
    }

    public void StopPowerFeather()
    {
        this.powerFeather = false;
    }

    public void StartPowerFeather()
    {
        this.powerFeather = true;
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            canGlide = false;
            animator.SetBool("isJump",false);
            animator.SetBool("isWalk",true);
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
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
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            animator.SetBool("isJump",true);
            if(powerFeather)
            {
                Invoke(nameof(CanGlide),1.5f);
            }
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
        if(Input.GetKeyDown(KeyCode.F) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed*directionShoot*Time.fixedDeltaTime,0f);
        newProjectile.transform.localScale = new Vector2(newProjectile.transform.localScale.x*directionShoot, newProjectile.transform.localScale.y );
        //Animacion de disparo si tenemos

        yield return new WaitForSeconds(shootTime);
        isShooting = false;

    }
    public bool GetSaveGame(){return this.saveGame;}

    public void SetSaveGame(bool saveGame){this.saveGame = saveGame;}

}
