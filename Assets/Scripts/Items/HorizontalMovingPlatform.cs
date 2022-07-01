    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    [SerializeField] private bool mustPatrol;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float speed;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private LayerMask wallLayer;
    private bool mustFlip;


    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
    }

    void FixedUpdate()
    {
        if(mustPatrol)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if(bodyCollider.IsTouchingLayers(wallLayer))
        {
            Flip();
        }
        rigidBody.velocity = new Vector2(speed*Time.fixedDeltaTime, 0);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x*-1,transform.localScale.y);
        speed *= -1;
        mustPatrol = true;
        
    }

    public Vector2 GetVelocity()
    {
        return rigidBody.velocity;
    }
}
