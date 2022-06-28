using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float speed;
    [SerializeField] private Transform startVerticalMovement;
    [SerializeField] private float secondsToDisapear;
    private bool reset;


    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,speed*Time.fixedDeltaTime);
        if(!reset)
        {
            StartCoroutine(ResetPlatform());
        }
    }

    IEnumerator ResetPlatform()
    {
        reset = true;
        yield return new WaitForSeconds(secondsToDisapear);
        reset = false;
        transform.position = startVerticalMovement.position;
    }
}
