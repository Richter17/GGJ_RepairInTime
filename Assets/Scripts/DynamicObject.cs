using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Editor;

[RequireComponent(typeof(Rigidbody2D))]
public class DynamicObject : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_initialForce;
    [SerializeField]
    private float m_initialRotation;
    private Rigidbody2D m_rigid;

    private void OnDrawGizmosSelected()
    {
        if(m_initialForce != Vector2.zero)
        DrawArrow.ForGizmo(transform.position, m_initialForce.normalized * Mathf.Lerp(0.1f, 2, m_initialForce.magnitude / 10), Color.blue);
        if(m_initialRotation != 0)
        {
            Vector3 dir = Quaternion.AngleAxis(90, Vector3.forward) * transform.up * (m_initialRotation/180) * 3;
            DrawArrow.ForGizmo(transform.position + transform.up.normalized, dir, Color.red);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_rigid.velocity = m_initialForce;
        m_rigid.angularVelocity = m_initialRotation;
    }
}
