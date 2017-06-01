using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWindowController : MonoBehaviour {

    private UIManager UiManager { get { return UIManager.instance; } }

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

    private void onCarChosen(int carNumber)
    {
        UIManager.ShowWindow(UiManager.LevelWindow);
        GameManager.SetCurrentPlayerCar(carNumber);
    }

    public void OnBackClicked()
    {
        UIManager.ShowWindow(UiManager.MainWindow);
    }
}
