using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; 

    private static int checkpointsScored = -1;
    private static int checkpointsCount = 0;
    private static float raceStartTime;
    private static int secodsToStartRace = 3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // UWAGA!!! To zadziała tylko w przypadku jednego levelu, tą inicjalizację trzeba wrzucić gdzieś indziej, póki co będzie tu.
            GameManager.checkpointsCount = GameObject.Find("checkpoints").GetComponentsInChildren(typeof(BoxCollider)).Length;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().enabled = false;
            
            Debug.Log(GameManager.checkpointsCount);
            initializeRace();
        }
        else if (instance != this)
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public static void checkCheckpoint()
    {
        if(checkpointsScored == 0) // if first checkpoint is to be counted
        {
            raceStartTime = Time.time;
        }
        checkpointsScored++;
        GameObject.Find("score_counter").GetComponent<Text>().text = string.Format("Checkpoints: {0}/{1}", GameManager.checkpointsScored, GameManager.checkpointsCount);
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
        GameObject.Find("start_counter").GetComponent<Text>().text = string.Format("{0}", secodsToStartRace);
        if (secodsToStartRace == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().enabled = true;
            instance.CancelInvoke("raceCountdown");
            GameObject.Find("start_counter").GetComponent<Text>().enabled = false;
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

            GameObject.Find("time_counter").GetComponent<Text>().text = timer_text;
        }
    }
}