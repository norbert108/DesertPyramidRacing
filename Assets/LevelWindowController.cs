using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWindowController : MonoBehaviour {

    private UIManager UiManager { get { return UIManager.instance; } }

    public void OnLevel1Clicked()
    {
        onLevelChosen(1);
    }

    public void OnLevel2Clicked()
    {
        onLevelChosen(2);
    }

    private void onLevelChosen(int levelNumber)
    {
        UIManager.LoadLevel(levelNumber);
    }

    public void OnBackClicked()
    {
        UIManager.ShowWindow(UiManager.CarWindow);
    }
}
