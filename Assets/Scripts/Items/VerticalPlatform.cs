using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update() 
    {
        RotateCollider();               
    }

    void RotateCollider()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            waitTime = 0.3f;
            effector.rotationalOffset = 0f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.3f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
