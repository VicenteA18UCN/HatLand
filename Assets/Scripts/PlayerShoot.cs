using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float shootSpeed, shootTime;
    [SerializeField] private Transform firePosition;
    [SerializeField] private GameObject projectile;
    private bool isShooting;
    private int direction;
    private bool firePower;

    void Start()
    {
        isShooting = false;
        direction = 1;
        firePower = false;
    }

    // Update is called once per frame  
    void Update()
    {
        ChangeDirection();
        if(Input.GetKeyDown(KeyCode.F) && !isShooting && firePower)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        GameObject newProjectile = Instantiate(projectile, firePosition.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed*direction*Time.fixedDeltaTime,0f);
        newProjectile.transform.localScale = new Vector2(newProjectile.transform.localScale.x*direction, newProjectile.transform.localScale.y );
        //Animacion de disparo si tenemos

        yield return new WaitForSeconds(shootTime);
        isShooting = false;

    }

    void ChangeDirection()
    {
        if(Input.GetKey(KeyCode.A))
        {
            direction = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            direction= 1;
        }
    }

    public void StopFirePower()
    {
        this.firePower = false;
    }

    public void StartFirePower()
    {
        this.firePower = true;
    }
}
