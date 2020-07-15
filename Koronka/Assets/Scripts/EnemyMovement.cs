using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skripta koja se dodaje protivnicima. Kreće se u okviru zadatog skupa tačaka.
public class EnemyMovement : MonoBehaviour
{
    /* 
     * Javna polja koja se podešavaju kao parametri skripte. 
     * Određuju dinamiku kretanja protivnika. To su brzina kretanja,
     * čekanje u jednoj tački pre promene kretanja kao i skup tačaka
     * u okviru kojih se kreće protivnik, odnosno ka njima.
     */
    public float speed;
    public Transform[] moveSpots;
    public float startWaitTime;

    private int randomSpot;
    private float waitTime;

    // Prilikom pokretanja skripte nasumično odrediti početnu tačku za kretanje.
    public void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    /* 
     * Osvežavanje položaja protivnika. 
     * Promena kretanja se dešava ukoliko se protivnik približi definisanoj tački. U tom trenutku čeka
     * u trajanju definisanim poljem startWaitTime, i onda odabere nasumično novu tačku za kretanje.*/
    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}