using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{
    [SerializeField]
    private Gradient m_lifeGrad;
    [SerializeField]
    [Range(0.001f, 0.025f)]
    private float m_speed = 0.015f;
    private Image m_img;

    private readonly Vector3 ROTATION = Vector3.forward * 90;
    // Start is called before the first frame update
    void Start()
    {
        m_img = transform.Find("Panel/Fill")?.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = ROTATION;
    }

    public void UpdateLife(float life)
    {
        StartCoroutine(AnimateLifeUpdate(life));
    }

    private IEnumerator AnimateLifeUpdate(float life)
    {
        float val = m_img.fillAmount;
        while (val > life)
        {
            val -= m_speed;
            m_img.fillAmount = val;
            m_img.color = m_lifeGrad.Evaluate(val);
            yield return null;
        }

        m_img.fillAmount = life;
        m_img.color = m_lifeGrad.Evaluate(life);
    }
}
