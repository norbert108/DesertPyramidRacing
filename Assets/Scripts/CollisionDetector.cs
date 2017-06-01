using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetector : MonoBehaviour
{
    private bool scored = false;

    void OnTriggerEnter(Collider other)
    {
        if(!scored)
        {
            scored = true;
            GameManager.checkCheckpoint();
        }
    }

}