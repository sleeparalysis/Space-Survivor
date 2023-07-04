using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private void Awake()
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
        m_XRange = 38.58f;
        m_YRange = 23.0f;
        m_Rotation = 0.0f;
       
        m_Target = GameObject.FindWithTag("Player"); ;
        m_AllowFire = true;
        m_Waiting = false;
    }

    void Start()
    {
        StartCoroutine(Wait(Random.Range(0.25f, 1.0f)));
    }

    void Update()
    {
        if (m_Experience > m_Required)
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
        if (m_Health > 0 && m_Target != null)
        {
            Rotate();
            Forward();


            if (m_AllowFire && !m_Waiting)
            {
                if (m_Stamina > 0)
                {
                    StartCoroutine(Shoot());
                }
                else
                {
                    StartCoroutine(Reload(m_Stamina / 10));
                }
            }
        }

        if(m_Target == null)
        {
            Forward();
        }

        if(m_RigidBody.velocity.magnitude > 0)
        {
            Constrain();
        }

        
    }

    protected override void Rotate()
    {
        Vector3 towardsPlayer = m_Target.transform.position - new Vector3(transform.position.x, transform.position.y, 0);
        if(transform.up != towardsPlayer)
        {
            transform.up = Vector3.RotateTowards(transform.up, towardsPlayer, 0.7f * Time.deltaTime, 0.0f);
        }
    }

    protected override void Constrain()
    {
        if (transform.position.x > m_XRange)
        {
            Die();
        }

        else if (transform.position.x < -m_XRange)
        {
            Die();
        }

        else if (transform.position.y > m_YRange)
        {
            Die();
        }

        else if (transform.position.y < -m_YRange)
        {
            Die();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }
}
