using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : BaseMonoBehaviour
{
    public Camera m_Camera;
    void Awake()
    {
        m_Camera = Camera.main;
    }


    void Update()
    {
        transform.LookAt(Position + m_Camera.transform.rotation * Vector3.back,
            m_Camera.transform.rotation * Vector3.up);
    }
}