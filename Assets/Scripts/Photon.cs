using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : Artillery
{
    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Vitality = 1;
        m_Health = m_Vitality;
        m_Level = 1;
        m_Required = 0;
        m_Experience = 0;
        m_Endurance = 0;
        m_Stamina = 0;
        m_Value = 0;
        m_Speed = 0;
        m_Acceleration = 2000.0f;
        m_XRange = 38.58f;
        m_YRange = 23.0f;
        m_Rotation = 0.0f;

        m_Damage = 1.0f;
        m_Cost = 1.0f;
        m_FireRate = 1.0f;
        m_Origin = null;
    }

    void Start()
    {
        Rotate();
        Forward();
    }

    protected override void Rotate()
    {
        transform.Rotate(0, 0, Random.Range(-m_Rotation, m_Rotation));
    }
}
