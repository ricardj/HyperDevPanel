using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPanelManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] bool _isDontDestroyOnLoad;
    [SerializeField] float _designWidth = 1080;
    [SerializeField] float _designHeigth = 1920;
    float _scrollViewDivider = 1.1f; //Somehow that works


    [Header("Debug values")]
    [SerializeField] bool showDevOptions = false;
    [SerializeField] List<IDevTool> devTools;
    [SerializeField] Vector2 scrollAmount = Vector2.zero;

    public void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        if (_isDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
    private void CheckRefreshTools()
    {
        if (devTools == null)
        {
            RefreshTools();
        }
    }

    public void RefreshTools()
    {
        devTools = FindObjectsOfType<MonoBehaviour>().OfType<IDevTool>().ToList();
    }
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        RefreshTools();
    }

    public void OnGUI()
    {
        SetupScreenSize();

        showDevOptions = GUILayout.Toggle(showDevOptions, "Show Dev Options");
        if (showDevOptions)
        {
            scrollAmount = GUILayout.BeginScrollView(scrollAmount, GUILayout.Height(_designHeigth / _scrollViewDivider));


            CheckRefreshTools();
            for (int i = 0; i < devTools.Count; i++)
            {
                IDevTool currentTool = devTools[i];
                GUILayout.Label(currentTool.GetToolName());
                currentTool.DrawOptions();
            }
            GUILayout.EndScrollView();
        }

    }

    private void SetupScreenSize()
    {
        float resX = (float)(Screen.width) / _designWidth;
        float resY = (float)(Screen.height) / _designHeigth;
        //Set matrix
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));
    }

}
