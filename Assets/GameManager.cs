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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // UWAGA!!! To zadziała tylko w przypadku jednego levelu, tą inicjalizację trzeba wrzucić gdzieś indziej, póki co będzie tu.
            GameManager.checkpointsCount = GameObject.Find("checkpoints").GetComponentsInChildren(typeof(BoxCollider)).Length;
            Debug.Log(GameManager.checkpointsCount);
            checkCheckpoint();
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
        if (checkpointsScored == checkpointsCount) win();
    }

    private static void win()
    {
        Debug.Log("ZWYCIENSTWO");
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