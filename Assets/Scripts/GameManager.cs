using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public List<GameObject> PlayerCars;
    public List<GameObject> PlayersDefaultCamera;

    public GameObject ScoreCounter;
    public GameObject TimeCounter;
    public GameObject StartCounter;

    public GameObject GameWindow;

    private GameObject playerCar;

    private static int playerCarNum = 1;
    private static int checkpointsScored = -1;
    private static int checkpointsCount = 0;
    private static float raceStartTime;
    private static int secodsToStartRace = 3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            checkpointsCount = GameObject.Find("checkpoints").GetComponentsInChildren<BoxCollider>().Length;
            initPlayerCar();
            playerCar.GetComponent<AbstractCarController>().enabled = false;
            
            Debug.Log("checkpoints: " + checkpointsCount);
            UIManager.ShowWindow(GameWindow);
            initializeRace();
        }
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void initPlayerCar()
    {
        playerCar = PlayerCars[playerCarNum];
        PlayerCars.ForEach(p => p.SetActive(false));
        playerCar.SetActive(true);
        var playerCamera = PlayersDefaultCamera[playerCarNum];
        PlayersDefaultCamera.ForEach(c => c.SetActive(false));
        playerCamera.SetActive(true);
    } 

    public static void checkCheckpoint()
    {
        if(checkpointsScored == 0) // if first checkpoint is to be counted
        {
            raceStartTime = Time.time;
        }
        checkpointsScored++;
        instance.ScoreCounter.GetComponent<Text>().text = string.Format("Checkpoints: {0}/{1}", GameManager.checkpointsScored, GameManager.checkpointsCount);
        if (checkpointsScored == checkpointsCount) raceFinished();
    }

    private static void raceFinished()
    {
        Debug.Log("ZWYCIENSTWO");
    }

    private static void initializeRace()
    {
        checkCheckpoint();
        instance.InvokeRepeating("raceCountdown", 1f, 1f);
    }

    private void raceCountdown()
    {
        StartCounter.GetComponent<Text>().text = string.Format("{0}", secodsToStartRace);
        if (secodsToStartRace == 0)
        {
            playerCar.GetComponent<AbstractCarController>().enabled = true;
            instance.CancelInvoke("raceCountdown");
            StartCounter.GetComponent<Text>().enabled = false;
        }
        secodsToStartRace--;
    }

    private void Update()
    {
        if(checkpointsScored > 0) // if race has started (first checkpoint passed)
        {
            float secondsSinceRaceStart = Time.time - raceStartTime;
            string minutes = Mathf.Floor(secondsSinceRaceStart / 60).ToString("00");
            string seconds = (secondsSinceRaceStart % 60).ToString("00");
            string timer_text = string.Format("{0}:{1}", minutes, seconds);

            TimeCounter.GetComponent<Text>().text = timer_text;
        }
    }

    public static void SetCurrentPlayerCar(int carNumber)
    {
        playerCarNum = carNumber;
    }
}