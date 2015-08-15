using UnityEngine;
using System.Collections.Generic;


// Testing some patterns to minimize restart bugs

/// <summary>
/// An singleton abstract component pattern
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonComponent<T> : MonoBehaviour 
    where T: SingletonComponent<T>
{

    #region instance part
    private static T m_pInstance = null;
    public static T GetInstance()
    {
        return m_pInstance;
    }

    protected virtual void Awake()
    {
        if (m_pInstance != null)
            Debug.LogError(name + " already initialized", this);
        m_pInstance = (T)this;
        SingletonManager.Add(m_pInstance);
    }
    #endregion
}



/// <summary>
/// A class to manager all singleton classes. yet testing
/// </summary>
public static class SingletonManager
{
    #region management part
    public static List<Object> m_pInstanceList = new List<Object>();

    /// <summary>
    /// stores a singleton intance
    /// </summary>
    /// <param name="comp"></param>
    public static void Add(Object comp)
    {
        m_pInstanceList.Add(comp);
    }
    /// <summary>
    /// removes a singleton intance
    /// </summary>
    /// <param name="comp"></param>
    public static void Remove(Object comp)
    {
        m_pInstanceList.Remove(comp);
    }

    /// <summary>
    /// clear a singleton intances
    /// </summary>
    public static void Clear()
    {
        for (int i = m_pInstanceList.Count - 1; i >= 0; i--)
            GameObject.Destroy(m_pInstanceList[i]);
        m_pInstanceList.Clear();
    }
    #endregion
}

/// <summary>
/// simple singleton method
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonClass<T>
    where T : SingletonClass<T> , new()
{
    protected static T m_pInstance = null;
    public static T GetInstance()
    {
        if (m_pInstance == null)
        {
            m_pInstance = new T();
        }
        return m_pInstance;
    }
}

