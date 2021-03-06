﻿using UnityEngine;
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

    public static int playerCarNum = 2;
    private static int checkpointsScored = -1;
    private static int checkpointsCount = 0;
    private static float raceStartTime;
    public static int secodsToStartRace = 0;
    public static int opponentsNumber = 0;
    public static int lapsFinished = 0;
    public static int totalLapsNumber = 1;

    void Awake()
    {
        if (instance == null)
        {
           // CarAudio
            instance = this;
            checkpointsCount = GameObject.Find("checkpoints").GetComponentsInChildren<BoxCollider>().Length;
            initPlayerCar();
            foreach (MonoBehaviour component in playerCar.GetComponents<MonoBehaviour>())
            {
                if (component is UnityStandardAssets.Vehicles.Car.CarAudio) continue;
                component.enabled = false;
            }

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
        Debug.Log("Wygrana");
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
            foreach (MonoBehaviour component in playerCar.GetComponents<MonoBehaviour>())
            {
                component.enabled = true;
            }
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
}