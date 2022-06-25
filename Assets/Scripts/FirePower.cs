using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : MonoBehaviour
{
    [SerializeField] private bool isGetted;

    private void OnEnable(){
        this.isGetted = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            this.isGetted=true;
        }
    }

    public bool GetStatus(){
        return isGetted;
    }

    public void ResetStatus(){
        this.isGetted = false;
    }
}

