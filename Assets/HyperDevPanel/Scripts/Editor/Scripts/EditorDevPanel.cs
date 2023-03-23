using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
namespace HyperDevTool
{

    [EditorTool("u Hyper shorcuts")]
    public class EditorDevPanel : EditorTool
    {
        [SerializeField] Texture2D _toolIcon;
        [SerializeField] DevOptionsListSO _devOptionsList;

        Dictionary<string, bool> dropdownTool = new Dictionary<string, bool>();
        List<IDevTool> tools;
        Vector2 scrollViewPosition = Vector2.zero;
        GUIContent m_IconContent;

        void OnEnable()
        {
            m_IconContent = new GUIContent()
            {
                image = _toolIcon,
            };

            EditorSceneManager.sceneLoaded += OnSceneChanged;
        }

        private void OnSceneChanged(Scene arg0, LoadSceneMode arg1)
        {
            RefreshFetchTools();
        }

        public override GUIContent toolbarIcon
        {
            get { return m_IconContent; }
        }


        [MenuItem("Custom Tools/Hyper Shorcuts _u")]
        public static void ActivateCustomTools()
        {
            Tools.current = Tool.Custom;

        }

        public override void OnToolGUI(EditorWindow window)
        {

            Handles.BeginGUI();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            RefreshTools();
            scrollViewPosition = EditorGUILayout.BeginScrollView(scrollViewPosition);
            DrawExtraOptions();
            DrawTools();
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.Space(500);
            GUILayout.EndHorizontal();
            Handles.EndGUI();
        }

        private void DrawExtraOptions()
        {
            if (_devOptionsList != null)
            {

                List<IDevOptionSO> devOptions = _devOptionsList.options;
                for (int i = 0; i < devOptions.Count; i++)
                {
                    IDevOptionSO currentDevOption = devOptions[i];
                    if (GUILayout.Button(currentDevOption.DevOptionName))
                    {
                        currentDevOption.ExecuteOption();
                    }
                }
            }
        }

        private void DrawTools()
        {

            for (int i = 0; i < tools.Count; i++)
            {
                IDevTool currentTool = tools[i];
                GUILayout.BeginVertical(GUI.skin.textArea);
                bool isUnfolded = CheckIfUnfolded(currentTool);

                if (isUnfolded)
                {
                    currentTool.DrawOptions();
                }
                GUILayout.EndVertical();
            }
        }

        private bool CheckIfUnfolded(IDevTool currentTool)
        {


            if (!dropdownTool.ContainsKey(currentTool.GetToolName()))
            {
                dropdownTool[currentTool.GetToolName()] = EditorPrefs.GetBool(currentTool.GetToolName(), false);
            }
            bool isUnfolded = dropdownTool[currentTool.GetToolName()];
            isUnfolded = EditorGUILayout.Foldout(isUnfolded, currentTool.GetToolName(), true);

            if (isUnfolded != dropdownTool[currentTool.GetToolName()])
            {
                EditorPrefs.SetBool(currentTool.GetToolName(), isUnfolded);
            }
            dropdownTool[currentTool.GetToolName()] = isUnfolded;

            return isUnfolded;
        }

        private void RefreshTools()
        {
            if (tools == null)
            {
                RefreshFetchTools();
            }

            if (GUILayout.Button("Refresh"))
            {
                RefreshFetchTools();
            }
        }

        private void RefreshFetchTools()
        {
            tools = FindObjectsOfType<MonoBehaviour>().OfType<IDevTool>().ToList();
        }
    }
}