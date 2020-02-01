using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthDepletedHandler();
public delegate void HitObjectHandler(Vector2 position, Transform trans);
public class ObjectHealth : MonoBehaviour
{
    [SerializeField]
    protected float m_health = 100;
    [SerializeField]
    protected float m_damageMultiplier = 1;
    public event HealthDepletedHandler HealthDepleted;
    public event HitObjectHandler HitObject;
    public float DamageMultiplier
    {
        get { return m_damageMultiplier; }
    }
    protected float m_remainingHealth;
    protected LifeBar m_lifeBar;
    public bool IsAlive { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        IsAlive = true;
        m_remainingHealth = m_health;
        m_lifeBar = GetComponentInChildren<LifeBar>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsAlive) return;
        ObjectHealth otherObj = other.gameObject.GetComponentInChildren<ObjectHealth>();
        float damage = other.relativeVelocity.magnitude;
        m_remainingHealth -= damage * (otherObj ? otherObj.DamageMultiplier : 1);
        HitObject?.Invoke(other.contacts[0].point, other.transform);
        m_lifeBar?.UpdateLife(m_remainingHealth / m_health);
        if (m_remainingHealth <= 0)
        {
            IsAlive = false;
            HealthDepleted?.Invoke();
            DestroyObject();
        }
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
