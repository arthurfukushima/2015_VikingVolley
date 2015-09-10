///////////////////////////////////////////////////////////
//  UIManager.cs
//  Implementation of the Class UIManager
//  Generated by Enterprise Architect
//  Created on:      19-dez-2014 14:59:43
//  Original author: Hugo
///////////////////////////////////////////////////////////


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreUI;
using Core;

namespace CoreUI
{
    /// <summary>
    /// Managers All kind of UIs, like popups and panels
    /// It has a list of instances and should be the main class to show and close UIs
    /// </summary>
    public class UIManager : SingletonComponent<UIManager>
    {
        private IAnalytics m_pAnalytics = null;
        private List<IBaseUI> m_pScreenHistory = new List<IBaseUI>();
        private Dictionary<System.Type, IBaseUI> m_pUIList = new Dictionary<Type, IBaseUI>();
        //public IBaseUI m_IBaseUI;

        #region gets
        public Dictionary<System.Type, IBaseUI> UIList
        {
            get { return m_pUIList; }
        }
        public IAnalytics Analytics
        {
            get { return m_pAnalytics; }
            set { m_pAnalytics = value; }
        }
        public List<IBaseUI> ScreenHistory
        {
            get { return m_pScreenHistory; }
        }
        #endregion

        protected void Start()
        {
            MonoBehaviour[] pList = GetComponentsInChildren<MonoBehaviour>(true);
            for (int i = 0; i < pList.Length; i++)
            {
                if (pList[i] is IBaseUI)
                {
                    m_pUIList.Add(pList[i].GetType(), (IBaseUI)pList[i]);
                    if (pList[i].gameObject.activeSelf)
                        ((IBaseUI)pList[i]).Show();
                }
            }
        }

        /// <summary>
        /// shows a UI if it is not visible and available
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pOnShow"></param>
        /// <returns></returns>
        public T Show<T>(OnUICompleteEvent pOnShow = null) where T : IBaseUI
        {
            IBaseUI pInstance = GetUIInstance<T>();
            if (pInstance != null && !pInstance.IsVisible())
            {
                pInstance.Show(pOnShow);
            }
            //Debug.Log("Show[" + typeof(T) + "] pInstance[" + (pInstance == null ? "null" : pInstance.ToString()) + "]", (pInstance == null ? null : ((MonoBehaviour)pInstance).gameObject));
            return (T)pInstance;
        }

        /// 
        /// <param name="pClassName"></param>
        /// <param name="pOnClose"></param>
        public void Close<T>(OnUICompleteEvent pOnClose = null) where T : IBaseUI
        {
            IBaseUI pInstance = GetUIInstance<T>();
            if (pInstance != null && pInstance.IsWorking())
            {
                pInstance.Close(pOnClose);
            }
        }
        public void NotifyOpen(IBaseUI pInstance)
        {
            if (pInstance.IsPage())
            {
                AddToHistory(pInstance);
                /*if(GetLastUI() != null)
                    GetLastUI().Show();*/
            }
        }
        public void NotifyClose(IBaseUI pInstance)
        {
            if (pInstance.IsPage())
            {
                if (GetCurrentUI() == pInstance)
                {
                    RemoveFromHistory(pInstance);
                    /*if(GetLastUI() != null)
                        GetLastUI().Show();*/
                }
                else
                    Debug.LogError("not the same.. GetCurrentUI[" + GetCurrentUI() + "] == pInstance[" + pInstance + "]");
            }
        }

        private void AddToHistory(IBaseUI pUI)
        {
            m_pScreenHistory.Add(pUI);
            if (Analytics != null)
                Analytics.TrackScreen(pUI.ToString());
        }
        private void RemoveFromHistory(IBaseUI pUI)
        {
            for (int i = m_pScreenHistory.Count - 1; i >= 0; i--)
            {
                if (m_pScreenHistory[i] == pUI)
                {
                    if (i >= 1 && Analytics != null)
                        Analytics.TrackScreen(m_pScreenHistory[i - 1].ToString());
                    m_pScreenHistory.RemoveAt(i);
                    break;
                }
            }
        }
        private IBaseUI GetCurrentUI()
        {
            if(m_pScreenHistory.Count > 0)
                return m_pScreenHistory[m_pScreenHistory.Count - 1];
            return null;
        }

        /// <summary>
        /// checks if the UI is already instantiated
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Constains<T>() where T : IBaseUI
        {
            return m_pUIList.ContainsKey(typeof(T));
        }

        /// <summary>
        /// gets the UI instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IBaseUI GetUIInstance<T>() where T : IBaseUI
        {
            if (Constains<T>())
            {
                IBaseUI pUI;
                m_pUIList.TryGetValue(typeof(T), out pUI);
                return pUI;
            }
            return null;
        }



        /*
        void OnGUI()
        {
            int i = 0;
            foreach (IBaseUI bu in UIList.Values)
            {
                string s = bu.ToString();
                if (GUI.Button(new Rect(0, i * 50, 150, 50), "Show[" + s + "]"))
                    bu.Show();
                if (GUI.Button(new Rect(150, i++ * 50, 150, 50), "Close[" + s + "]"))
                    bu.Close();
            }
        }
        */

    }//end UIManager

}//end namespace CoreUI