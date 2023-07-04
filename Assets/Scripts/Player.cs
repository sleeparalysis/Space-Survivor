using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : Unit
{
    protected void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Vitality = 1;
        m_Health = m_Vitality;
        m_Level = 1;
        m_Required = 1;
        m_Experience = 0;
        m_Endurance = 10;
        m_Stamina = m_Endurance;
        m_Value = 1;
        m_Speed = 0;
        m_Acceleration = 10.0f;
        m_XRange = 33.58f;
        m_YRange = 18.0f;
        m_Rotation = 0.0f;
        
        m_Target = null;
        m_AllowFire = true;
        m_Waiting = false;
    }

    void Start()
    {
        Level = 99;
    }

    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && m_AllowFire)
        {
            if (m_Stamina > 0)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                StartCoroutine(Reload(m_Endurance / 100));
            }
        }

        if (m_Experience >= m_Required)
        {
            LevelUp();
        }

        if (m_Health <= 0)
        {
            m_Health = 0;
            Die();
        }
    }

    void FixedUpdate()
    {
        if (m_Health > 0)
        {
            Rotate();
            Constrain();

            if (Input.GetKey(KeyCode.W))
            {
                Up();
            }

            if (Input.GetKey(KeyCode.A))
            {
                Left();
            }

            if (Input.GetKey(KeyCode.S))
            {
                Down();
            }

            if (Input.GetKey(KeyCode.D))
            {
                Right();
            }
        }
    }

    protected override void Constrain()
    {
        if (transform.position.x > m_XRange)
        {
            transform.position = new Vector2(m_XRange, transform.position.y);
            m_RigidBody.velocity = new Vector2(0.0f, m_RigidBody.velocity.y);
        }

        else if (transform.position.x < -m_XRange)
        {
            transform.position = new Vector2(-m_XRange, transform.position.y);
            m_RigidBody.velocity = new Vector2(0.0f, m_RigidBody.velocity.y);
        }

        else if (transform.position.y > m_YRange)
        {
            transform.position = new Vector2(transform.position.x, m_YRange);
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 0.0f);
        }

        else if (transform.position.y < -m_YRange)
        {
            transform.position = new Vector2(transform.position.x, -m_YRange);
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 0.0f);
        }
    }

    protected override void Rotate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            m_Health -= collision.gameObject.GetComponent<Enemy>().Health;
        }
    }
}