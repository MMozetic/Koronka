using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skripta koja se postavlja na Protivnike i elemente koji mogu da oduzmu život igraču.
public class RespawnPlayer : MonoBehaviour
{
    public Transform spawnPoint;

    /* 
     * Ukoliko se desi kolizija sa igračem, oduzeti mu život
     * i ponovo ga postaviti na poziciju za oživljavanje.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            other.transform.position = spawnPoint.transform.position;
            ScoreController.instance.DecreaseNumberOfLives();
        }
    }
}
