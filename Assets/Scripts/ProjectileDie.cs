using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDie : MonoBehaviour
{
   [SerializeField] private GameObject ProjectileEffect;
   [SerializeField] private float dieTime;

    void Start()
    {
        StartCoroutine(Timer());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisonGameObject = other.gameObject;
        if(!collisonGameObject.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        if(ProjectileEffect != null)
        {
            Instantiate(ProjectileEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }
}
