using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour
{
    [SerializeField] protected GameObject m_ParticleSystem;
    [SerializeField] protected int m_Level;
    [SerializeField] protected int m_Experience;
    [SerializeField] protected int m_Required;
    [SerializeField] protected int m_Value;
    [SerializeField] protected float m_Vitality;
    [SerializeField] protected float m_Health;
    [SerializeField] protected float m_Endurance;
    [SerializeField] protected float m_Stamina;
    [SerializeField] protected float m_Speed;
    [SerializeField] protected float m_Acceleration;
    [SerializeField] protected float m_XRange;
    [SerializeField] protected float m_YRange;
    [SerializeField] protected float m_Rotation;
    protected Rigidbody2D m_RigidBody;

    

    public virtual int Level
    {
        get { return m_Level; }
        set
        {
            m_Level = value;

            for (int i = 0; i < m_Level; i++)
            {
                m_Vitality++;
                m_Endurance += 10;
                m_Required++;
                m_Acceleration++;
                m_Value++;
            }

            m_Health = m_Vitality;
            m_Stamina = m_Endurance;
            m_Experience = 0;
        }
    }

    public int Experience
    {
        get { return m_Experience; }
        set { m_Experience = value; }
    }

    public int Required
    {
        get { return m_Required; }
        set { m_Required = value; }
    }

    public int Value
    {
        get { return m_Value; }
        set { m_Value = value; }
    }

    public float Vitality
    {
        get { return m_Vitality; }
        set { m_Vitality = value; }
    }

    public float Health
    {
        get { return m_Health; }
        set { m_Health = value; }
    }

    public float Endurance
    {
        get { return m_Endurance; }
        set { m_Endurance = value; }
    }

    public float Stamina
    {
        get { return m_Stamina; }
        set { m_Stamina = value; }
    }

    public virtual void LevelUp()
    {
        m_Level++;
        m_Vitality++;
        m_Endurance++;
        m_Required++;
        m_Acceleration++;
        m_Value++;

        m_Health = m_Vitality;
        m_Stamina = m_Endurance;
        m_Experience = 0;
    }

    public void Die()
    {
        Destroy(gameObject);
        Explode();
    }

    protected void Up()
    {
        Vector2 up = new Vector2(0, 1);
        m_RigidBody.AddForce(up * m_Acceleration, ForceMode2D.Force);
    }

    protected void Down()
    {
        Vector2 down = new Vector2(0, -1);
        m_RigidBody.AddForce(down * m_Acceleration, ForceMode2D.Force);
    }

    protected void Left()
    {
        Vector2 left = new Vector2(-1, 0);
        m_RigidBody.AddForce(left * m_Acceleration, ForceMode2D.Force);
    }

    protected void Right()
    {
        Vector2 right = new Vector2(1, 0);
        m_RigidBody.AddForce(right * m_Acceleration, ForceMode2D.Force);
    }

    protected void Forward()
    {
        m_RigidBody.AddForce(transform.up * m_Acceleration, ForceMode2D.Force);
    }

    protected void Backward()
    {
        m_RigidBody.AddForce(transform.up * -m_Acceleration, ForceMode2D.Force);
    }

    protected void Explode()
    {
        GameObject Explosion = Instantiate(m_ParticleSystem, transform.position, transform.rotation);
        ParticleSystem ps = Explosion.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;

        psmain.startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    

    protected abstract void Rotate();
    protected abstract void Constrain();
}
