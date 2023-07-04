using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Object
{
    [SerializeField] protected GameObject m_Target;
    [SerializeField] protected GameObject m_Artillery;
    [SerializeField] protected bool m_AllowFire;
    [SerializeField] protected bool m_Waiting;

    protected IEnumerator Shoot()
    {
        m_AllowFire = false;

        GameObject clone = Instantiate(m_Artillery, transform.position + (transform.up * 1.5f), transform.rotation);

        clone.GetComponent<Artillery>().Origin = gameObject;
        clone.GetComponent<Artillery>().Level = m_Level;
        clone.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;

        m_Stamina -= clone.GetComponent<Artillery>().Cost;
        float rate = clone.GetComponent<Artillery>().FireRate;

        yield return new WaitForSeconds(rate);
        m_AllowFire = true;
    }

    protected IEnumerator Reload(float seconds)
    {
        m_AllowFire = false;
        m_Waiting = true;
        yield return new WaitForSeconds(seconds);
        m_Stamina = m_Endurance;
        m_Waiting = false;
        m_AllowFire = true;
    }

    protected IEnumerator Wait(float seconds)
    {
        m_Waiting = true;
        yield return new WaitForSeconds(seconds);
        m_Waiting = false;
    }
}
