using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Klasa za kontrolu izbora igrača. 
 * Funkcije se dodeljuju onClick komponentama za dugmiće.
 * Koristi korisničke preference da zapamti koji je karakter odabran.
 */
public class CharacterChoose : MonoBehaviour
{
    // Javna polja za promenu pozadine prilikom odabira karaktera.
    public GameObject background1;
    public GameObject background2;

    // Prilikom pokretanja, odabran podrazumevani karakter.
    public void Start()
    {
        PlayerPrefs.SetString("Character", "Girl");
        background1.SetActive(true);
        background2.SetActive(false);
    }

    // Reagovanje na pritisak dugmeta za odabir prvog karaktera.
    public void SelectCharacter1()
    {
        PlayerPrefs.SetString("Character", "Girl");
        background1.SetActive(true);
        background2.SetActive(false);
    }

    // Reagovanje na pritisak dugmeta za odabir drugog karaktera.
    public void SelectCharacter2()
    {
        PlayerPrefs.SetString("Character", "Boy");
        background1.SetActive(false);
        background2.SetActive(true);
    }
}
