using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Klasa koja je iskorišćena za pračenje karaktera.
 * Dodata je objektu MainCamera. Preuzeta je iz podrške 
 * koju nudi Unity zajednica i prilagođena za potrebe zadatka.
 */
public class CameraTracking : MonoBehaviour
{
    private Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPos;

    // Javna polja za povezivanje karaktera i pozadine radi pristupa kroz skriptu.
    public GameObject character1;
    public GameObject character2;
    public GameObject background1;
    public GameObject background2;

    /*
     * Prilikom inicijalizacije kamere, potrebno je utvrditi
     * koji karakter je odabran u početnom meniju. Ukloniti
     * karakter koji nije odabran kao i njegovu pozadinu.
     * Podesiti polje target za praćenje na transform polje 
     * objekta odabranog karaktera.
     */
    public void Start()
    {
        if (PlayerPrefs.GetString("Character") == "Girl")
        {
            Destroy(character2.gameObject);
            Destroy(background2.gameObject);
            target = character1.gameObject.transform;
        }
        else
        {
            Destroy(character1.gameObject);
            Destroy(background1.gameObject);
            target = character2.gameObject.transform;
        }

        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

    // Osvežavanje kamere jednom u frejmu.
    public void Update()
    {
        float xMoveDelta = (target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;

        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

        transform.position = newPos;

        lastTargetPosition = target.position;
    }
}
