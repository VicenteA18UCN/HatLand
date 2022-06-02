using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerVelocity;
    private float jumpForce;
    private bool isJumping;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D playerRigidBody;

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
        this.jumpForce = 8.5f;
        this.isJumping = false;
    }

    // Update is called one per frame
    private void Update()
    {
        this.PlayerAnimations();
        this.PlayerJump();
    }

    void FixedUpdate()
    {
        this.PlayerMovements();
    }

    void PlayerMovements()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(playerVelocity,0,0);
            spriteRenderer.flipX = false;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
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
        }
    }

    void PlayerAnimations()
    {
        if(this.isJumping)
        {
            animator.SetBool("isWalk",false);
            return;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetBool("isWalk",true);
        }
        if(!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isWalk",false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            this.isJumping = false;
            animator.SetBool("isJump",false);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            this.isJumping = true;
            animator.SetBool("isJump",true);
        }
    }
}
