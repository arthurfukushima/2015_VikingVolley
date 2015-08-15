using UnityEngine;
using System.Collections;

/// <summary>
/// base optimized class 
/// </summary>
public class BaseMonoBehaviour : MonoBehaviour
{
    private Transform m_pTransform;
    private GameObject m_pGameObject;
    private Animation m_pAnimation;
    private RectTransform m_pRectTransform;



    #region gets/sets

    /// <summary>
    /// Optimized RectTransform getter. Try to get cached one or find and store one to cache
    /// It is a subclass of Transform and are used for UIs
    /// </summary>
    public RectTransform RectTransform
    {
        get {
            if (m_pRectTransform == null)
                m_pRectTransform = (RectTransform)Transform;
            return m_pRectTransform; 
        }
        set { m_pRectTransform = value; }
    }
    /// <summary>
    /// Optimezed Transform getter. Try to get cached one or find and store one to cache
    /// </summary>
    public Transform Transform
    {
        get
        {
            if (m_pTransform == null)
                m_pTransform = transform;// GetComponent<Transform>();
            return m_pTransform;
        }
        set { m_pTransform = value; }
    }
    /// <summary>
    /// Optimezed Animation getter. Try to get cached one or find and store one to cache
    /// </summary>
    public Vector3 Position
    {
        get { return Transform.position; }
        set { Transform.position = value; }
    }

    /// <summary>
    /// Optimezed GameObject getter. Try to get cached one or find and store one to cache
    /// </summary>
    public GameObject GameObject
    {
        get
        {
            if (m_pGameObject == null)
                m_pGameObject = gameObject;// GetComponent<GameObject>();
            return m_pGameObject;
        }
        set { m_pGameObject = value; }
    }
    /// <summary>
    /// Optimezed Animation getter. Try to get cached one or find and store one to cache
    /// </summary>
    public Animation Animation
    {
        get
        {
            if (m_pAnimation == null)
                m_pAnimation = GetComponent<Animation>();
            return m_pAnimation;
        }
        set { m_pAnimation = value; }
    }
    #endregion

}
