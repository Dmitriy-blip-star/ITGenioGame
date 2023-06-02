using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rb;
    static SpriteRenderer spriteRend;
    public GameObject shootPoint;
    public GameObject bullet;
    Vector3 bullPos;
    Animator anim;

    bool isGround = true;
    public float speed = 5f;
    public float jump = 25f;
    public static int health = 100;
    public float dethTime = 2f;
    int damage = 20;
    int medication = 30;

    static public bool lookRight = false;
    
    bool isRun = false;
    bool canShoot = true;

    public Image hp;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();

        
    }

    void Update()
    {
        

        anim.SetBool("isRun", isRun);
        anim.SetBool("isGround", isGround);
        anim.SetInteger("Helth", health);

        float move = Input.GetAxis("Horizontal"); // ��������� �������� �� -1 �� 1 ������������ ����������� ��������

        

        if (health <= 0)
        {
            StartCoroutine(Deth());
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey("right")) // �������� �� ������� �������, ��������� � ����������� �� �����������, ������� � ������ ��������
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            spriteRend.flipX = false;
            isRun = true;            
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left")) // �������� �� ������� �������, ��������� � ����������� �� �����������, ������� � ������ ��������
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            spriteRend.flipX = true;
            isRun = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround) // �������� �� ������� ������� � ���������� isGround, ��������� �� ��� �
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp("right") || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp("left"))
        {
            isRun = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && canShoot) // ���������� canSho�t ������ �������� ����� �������� � � �������. 
        {
            bullPos.x = transform.position.x + 0.7f; // ����� ���� �� ����������� ���� ������ �� ����� � ������� � ������, ����� ���� �����
            bullPos.y = transform.position.y;
            bullPos.z = transform.position.z - 3;
            Instantiate(bullet, transform.position, shootPoint.transform.rotation); // �������� �������� rotation ������� � ������� shootPoint
            canShoot= false;
            StartCoroutine(FireCoolDown());

        }

        if (move > 0 && !lookRight) // �������� � ������� ��� ������������� ������� shootP�int
        {
            Flip();
        }
        else if (move < 0 && lookRight)
        {
            Flip();
        }
    }

    IEnumerator FireCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canShoot= true;
    } // ����������(�������)

    IEnumerator Deth()
    {
        yield return new WaitForSeconds(dethTime);
        health = 100;
        SceneManager.LoadScene(0);
    }

    
    private void Flip() // ����� ��� �������� ������� shootPoint
    {
        lookRight = !lookRight; // �������� �� ����������� ��������
        shootPoint.transform.Rotate(0, 180f, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {            
            health -= damage;
            hp.fillAmount -= damage / 100f;
        }
    } // �������� �� ��������������� � ������ ��� ���������� �������� ������

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HealthPotion")
        {
            health += medication;
            Destroy(collision.gameObject);
            hp.fillAmount += medication / 100f;
        }
    }
}
