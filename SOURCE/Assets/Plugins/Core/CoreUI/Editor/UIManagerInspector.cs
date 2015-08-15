#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;


namespace CoreUI
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerInspector : Editor
    {
        private UIManager m_pUIManager;

        protected void OnEnable()
        {
            m_pUIManager = (UIManager)target;
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            GUILayout.BeginVertical();
            if (m_pUIManager.UIList.Count > 0)
            {
                GUILayout.Label("---- UI List:");
                foreach (Type gui in m_pUIManager.UIList.Keys)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.ObjectField(gui.ToString(), ((MonoBehaviour)m_pUIManager.UIList[gui]).gameObject, typeof(MonoBehaviour));
                    GUILayout.EndHorizontal();
                }

                GUILayout.Label("---- UI History");
                foreach (IBaseUI gui in m_pUIManager.ScreenHistory)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.ObjectField(gui.ToString(), ((MonoBehaviour)gui).gameObject, typeof(MonoBehaviour));
                    GUILayout.EndHorizontal();
                }
            }
            else
                GUILayout.Label("Has no one instance");

            GUILayout.EndVertical();
        }
	
    }
}
#endif