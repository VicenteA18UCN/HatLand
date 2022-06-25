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

    void Start()
    {
        isShooting = false;
        direction = 1;
    }

    // Update is called once per frame  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !isShooting)
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

    public void setDirection(int direction){this.direction = direction;}
    public int getDirection(){return this.direction;}
}
