using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Artillery : Object
{
    [SerializeField] protected float m_Damage;
    [SerializeField] protected float m_Cost;
    [SerializeField] protected float m_FireRate;
    protected GameObject m_Origin;

    public override int Level
    {
        get { return m_Level; }
        set
        {
            m_Level = value;
            for (int i = 0; i < m_Level; i++)
            {
                m_Rotation += 0.05f;
                m_Damage += 0.1f;
                m_FireRate -= 0.0095f;
            }
        }
    }

    public float Damage
    {
        get { return m_Damage; }
        set { m_Damage = value; }
    }

    public float Cost
    {
        get { return m_Cost; }
        set { m_Cost = value; }
    }

    public float FireRate
    {
        get { return m_FireRate; }
        set { m_FireRate = value; }
    }

    public GameObject Origin
    {
        get { return m_Origin; }
        set { m_Origin = value; }
    }

    private void FixedUpdate()
    {
        if (m_Health > 0)
        {
            Constrain();
        }
        else
        {
            m_Health = 0;
            Die();
        }
    }

    protected override void Constrain()
    {
        if (transform.position.x > m_XRange ||
            transform.position.x < -m_XRange ||
            transform.position.y > m_YRange ||
            transform.position.y < -m_YRange)
        {
            Die();
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Object Target = collision.gameObject.GetComponent<Object>();
            int value = Target.Value;
            Target.Health -= m_Damage;
            m_Health--;

            if (Origin != null && Target.Health <= 0)
            {
                Origin.GetComponent<Object>().Experience += value;
            }
        }
    }
}
