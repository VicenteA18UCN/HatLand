using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void FixedUpdate() 
    {
        if(Input.GetKey(KeyCode.S))
        {
             effector.rotationalOffset = 180f;
        }
               
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            effector.rotationalOffset = 0f;
        }
    } 
}
