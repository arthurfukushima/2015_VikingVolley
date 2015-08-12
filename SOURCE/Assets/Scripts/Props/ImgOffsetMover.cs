using UnityEngine;
using System.Collections;

public class ImgOffsetMover : MonoBehaviour
{
    public float m_Velocity;
    public Renderer m_Renderer;
    private Material m_Material;
    void Start()
    {
        if(m_Renderer)
            m_Material = m_Renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Material == null)
            return;

        Vector2 v = m_Material.GetTextureOffset("_MainTex");
        v.x += m_Velocity;
        if (v.x > 1)
        {
            v.x = 0;
        }
        m_Material.SetTextureOffset("_MainTex", v);

    }
}