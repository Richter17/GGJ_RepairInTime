using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndHueScreen : MonoBehaviour
{
    [SerializeField]
    private float m_hueAnimationSpeed = 0.01f;
    private Image m_img;

    public Color Color
    {
        get { return m_img.color; }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        m_img = GetComponent<Image>();
        StartCoroutine(HueAniamtion());
    }

    private IEnumerator HueAniamtion()
    {
        while (true)
        {
            Color.RGBToHSV(m_img.color, out float h, out float s, out float v);
            h += m_hueAnimationSpeed;
            m_img.color = Color.HSVToRGB(h, s, v);
            yield return null;
            yield return null;
        }
    }
}
