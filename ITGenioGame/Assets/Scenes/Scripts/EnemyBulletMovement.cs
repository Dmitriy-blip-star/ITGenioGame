using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBulletMovement : MonoBehaviour
{


    Rigidbody2D rb;
    public float speed = 15f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        //StartCoroutine(WaitBeforeDel()); // Запуск таймера
    }


    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy") && tag != "EnemyBullet")
        {
          //  Destroy(gameObject);
        }

    }

    IEnumerator WaitBeforeDel() // Инициализация таймера
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
