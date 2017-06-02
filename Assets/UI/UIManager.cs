using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject MainWindow;
    public GameObject SelectionWindow;
    public GameObject OptionsWindow;
    public GameObject HelpWindow;
    public GameObject AboutWindow;

    public GameObject GameWindow;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ShowMainWindow()
    {
        ShowWindow(MainWindow);
    }

    public static void ShowWindow(GameObject window)
    {
        HideAllWindows();
        window.SetActive(true);
    }

    public static void HideWindow(GameObject window)
    {
        window.SetActive(false);
    }

    private static void HideAllWindows()
    {
        try
        {
            var allWindows = new List<GameObject> { instance.MainWindow, instance.SelectionWindow, instance.OptionsWindow, instance.HelpWindow, instance.AboutWindow };
            allWindows.ForEach(HideWindow);
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Menu windows not initialized");
            Debug.Log(e.StackTrace);
        }
    }

    public static void LoadLevel(int levelNumber)
    {
        HideAllWindows();
        SceneManager.LoadScene("Level" + levelNumber);
    }

    public static UIManager instance;
}
