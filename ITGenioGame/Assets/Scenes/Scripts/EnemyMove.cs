using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    static int health = 2;
    Animator anim;
    int speed = 5;
    Rigidbody2D rb;
    Vector2 lastPosition;
    //public GameObject gun;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        lastPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Helth", health);
        rb.velocity = transform.right * speed;
        if (lastPosition.x < transform.position.x - 6 && transform.right.x > 0)
        {
            lastPosition.x = transform.position.x;
            transform.Rotate(0, 180f, 0);
            //gun.transform.Rotate(0, 180f, 0);
        }
        else if (lastPosition.x > transform.position.x + 6 && transform.right.x < 0)
        {
            lastPosition.x = transform.position.x;
            transform.Rotate(0, 180f, 0);
            //gun.transform.Rotate(0,180f,0);
        }

        if (health <= 0)
        {
            rb.velocity = transform.position * 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
        }
        if (health <= 0)
        {
            StartCoroutine(DeathTime());
        }
    }
    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}