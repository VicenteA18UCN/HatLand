using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinManager skinManager;
    private float playerVelocity;
    private float jumpForce;
    private bool isJumping;
    private Animator animator;
    [SerializeField] private bool isDead;
    [SerializeField] private bool saveGame;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D playerRigidBody;
    private bool canGlide;
    private bool powerFeather;
    [SerializeField] private RuntimeAnimatorController[] animators;

    private void OnEnable()
    {
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.playerRigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.playerVelocity = 0.1f;
        this.jumpForce = 8.2f;
        this.isJumping = false;
        this.canGlide = false;
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
    }

    void FixedUpdate()
    {
        this.PlayerMovements();
    }

    void PlayerMovements()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(playerVelocity,0,0);
            spriteRenderer.flipX = false;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-playerVelocity,0,0);
            spriteRenderer.flipX = true;
        }
    }

    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playerRigidBody.AddForce(new Vector3 (0,jumpForce,0),ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    void PlayerAnimations()
    {
        if(this.isJumping)
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
            if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            Vector2 force = Vector2.up * 0.39f;
            playerRigidBody.AddForceAtPosition(force, transform.position);
        }
        }
        }

    }

    void CanGlide()
    {
        this.canGlide = true;
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
            this.isJumping = false;
            this.canGlide = false;
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
            this.isJumping = true;
            animator.SetBool("isJump",true);
            if(powerFeather)
            {
                Invoke(nameof(CanGlide),0.9f);
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

    public bool GetSaveGame(){return this.saveGame;}

    public void SetSaveGame(bool saveGame){this.saveGame = saveGame;}

}
