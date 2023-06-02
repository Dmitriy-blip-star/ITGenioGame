using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootZone : MonoBehaviour
{
    public GameObject bullet;
    Vector3 bulPos;
    
    bool canShoot = true;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) // Enter -> Stay(?)
    {
        bulPos.z = transform.position.z - 3;

        if (collision.tag == "Player" && canShoot)
        {
            Instantiate(bullet, bulPos, transform.rotation);
            canShoot = false;
            StartCoroutine(FireCoolDown());
        }
    }

    IEnumerator FireCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    } // нумераторы(таймеры)
}
