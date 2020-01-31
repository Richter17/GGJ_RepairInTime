using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField]
    protected float m_health = 100;
    [SerializeField]
    protected float m_damageMultiplier;
    public float DamageMultiplier
    {
        get { return m_damageMultiplier; }
    }
    protected float m_remainingHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_remainingHealth = m_health;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ObjectHealth otherObj = other.gameObject.GetComponentInChildren<ObjectHealth>();
        float damage = other.relativeVelocity.magnitude;
        m_remainingHealth -= damage  * (otherObj ? otherObj.DamageMultiplier : 1);

        if (m_health <= 0)
            DestroyObject();
        else
            CrackEffect(m_remainingHealth / m_health);
    }

    protected virtual void DestroyObject()
    {
        Debug.Log("Destroy " + gameObject.name);
    }

    protected virtual void CrackEffect(float healthRatio)
    {
        Debug.Log("Crack " + gameObject.name);
    }
}
