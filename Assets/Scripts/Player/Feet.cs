using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public bool isJumping;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            this.isJumping = false;
            Player.canGlide = false;
        }
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {
            this.isJumping = false;
            Player.canGlide = false;
        }
        if(other.gameObject.CompareTag("MovingOneWayPlatform"))
        {
            this.isJumping = false;
            Player.canGlide = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Platform"))
        {
            this.isJumping = true;
        }
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {
            this.isJumping = true;
        }
        if(other.gameObject.CompareTag("MovingOneWayPlatform"))
        {
            this.isJumping = true;
        }
    }
}
