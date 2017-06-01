using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWindowController : MonoBehaviour {

    private UIManager UiManager { get { return UIManager.instance; } }
    private int chosenLevel = 1;
    private int chosenCar = 0;
    private Color chosenColor = new Color(0.45f, 1.0f, 0.52f);

    public void Start()
    {
        GameObject.Find("Level" + chosenLevel + "Panel").GetComponent<UnityEngine.UI.Image>().color = chosenColor;
        GameObject.Find("Car" + (chosenCar + 1) + "Panel").GetComponent<UnityEngine.UI.Image>().color = chosenColor;
    }

    public void OnCar1Clicked()
    {
        onCarChosen(0);
    }

    public void OnCar2Clicked()
    {
        onCarChosen(1);
    }

    public void OnCar3Clicked()
    {
        onCarChosen(2);
    }

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
        for (int i = 1; i < 3; i++)
        {
            if (i == levelNumber) GameObject.Find("Level" + i + "Panel").GetComponent<UnityEngine.UI.Image>().color = chosenColor;
            else GameObject.Find("Level" + i + "Panel").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
        chosenLevel = levelNumber;
    }

    private void onCarChosen(int carNumber)
    {
        for(int i = 0; i < 3; i++)
        {
            if(i == carNumber) GameObject.Find("Car" + (i + 1) + "Panel").GetComponent<UnityEngine.UI.Image>().color = chosenColor;
            else GameObject.Find("Car" + (i + 1) + "Panel").GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }

        chosenCar = carNumber;
    }

    public void OnBackClicked()
    {
        UIManager.ShowWindow(UiManager.MainWindow);
    }

    public void OnStartClicked()
    {
        GameManager.playerCarNum = chosenCar;
        GameManager.opponentsNumber = GameObject.Find("OpponentsDropdown").GetComponent<UnityEngine.UI.Dropdown>().value;
        GameManager.totalLapsNumber = GameObject.Find("LapsDropdown").GetComponent<UnityEngine.UI.Dropdown>().value;
        UIManager.LoadLevel(chosenLevel);
    }
}
