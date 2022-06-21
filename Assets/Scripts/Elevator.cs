using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //private float movementSpeed;
    [SerializeField] private float dontFall;
    [SerializeField] private GameObject elevator;

    private void OnEnable()
    {
        //this.movementSpeed = 10f;
        this.elevator = GameObject.FindGameObjectWithTag("Elevator");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            this.elevator.SetActive(false);
        }
    }

    // private void OnCollisionExit2D()
    // {
    //     this.elevator.SetActive(false);
    //     this.elevator.SetActive(true);
    // }
}
