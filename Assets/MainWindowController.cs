using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindowController : MonoBehaviour {

    private UIManager UiManager { get { return UIManager.instance; } }

    public void OnPlayClicked()
    {
        Debug.Log("Play clicked");
        UIManager.ShowWindow(UiManager.CarWindow);
    }

    public void OnOptionsClicked()
    {
        UIManager.ShowWindow(UiManager.OptionsWindow);
    }

    public void OnHelpClicked()
    {
        UIManager.ShowWindow(UiManager.HelpWindow);
    }

    public void OnAboutClicked()
    {
        UIManager.ShowWindow(UiManager.AboutWindow);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

}
