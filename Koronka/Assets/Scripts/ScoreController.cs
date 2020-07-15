using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Klasa koja kontroliše rezultat kao i život korisnika i prikaz na UI.
public class ScoreController : MonoBehaviour
{
    // Statička instanca klase, tako da postoji samo jedna instanca u celoj aplikaciji. (Singleton Class Pattern)
    public static ScoreController instance;

    /* Javna polja klase služe za povezivanje sa UI elementima, odnosno brojačima.
     * Moguć je unos broja života u igri. Takođe je moguće i postaviti objekte koji
     * se prikazuju korisniku ukoliko pobedi odnosno izgubi igru. */
    private int scoreForDisinfectant;
    private int scoreForSoap;
    private int scoreForMask;
    private int scoreForGloves;
    public Text textForDisinfectant;
    public Text textForSoap;
    public Text textForMask;
    public Text textForGloves;

    private int lifeCounter;
    public int maximumNumberOfLives;
    public Text textForLives;

    public GameObject gameOverUI;
    public GameObject gameWinUI;

    // Ukupan broj elemenata za kontrolu pobede u igri.
    private int discinfectantCount;
    private int soapCount;
    private int maskCount;
    private int glovesCount;

    // Funkcija za inicijalizaciju Singleton instance klase. Inicijalizacija teksta UI elemenata.
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
            lifeCounter = maximumNumberOfLives;
            textForLives.text = lifeCounter.ToString();

            discinfectantCount = GameObject.FindGameObjectsWithTag("disinfectant").Length;
            soapCount = GameObject.FindGameObjectsWithTag("soap").Length;
            maskCount = GameObject.FindGameObjectsWithTag("mask").Length;
            glovesCount = GameObject.FindGameObjectsWithTag("gloves").Length;
        }
    }

    // Kontrola rezultata i prikaz na UI.
    public void ChangeScore(string scoreTag)
    {
        switch (scoreTag)
        {
            case "disinfectant":
                scoreForDisinfectant++;
                textForDisinfectant.text = scoreForDisinfectant.ToString();
                break;
            case "soap":
                scoreForSoap++;
                textForSoap.text = scoreForSoap.ToString();
                break;
            case "mask":
                scoreForMask++;
                textForMask.text = scoreForMask.ToString();
                break;
            case "gloves":
                scoreForGloves++;
                textForGloves.text = scoreForGloves.ToString();
                break;
            default:
                break;
        }

        CheckIfCollectedAllElements();
    }

    // Ukoliko su svi elementi sakupljeni, poziva se korutina koja definiše ponašanje za pobedu.
    private void CheckIfCollectedAllElements()
    {
        if (scoreForDisinfectant == discinfectantCount &&
            scoreForSoap == soapCount &&
            scoreForMask == maskCount &&
            scoreForGloves == glovesCount)
        {
            StartCoroutine(gameWin());
        }
    }

    /* 
     * Smanjuje broj života, osvežava UI element.
     * Ukoliko se izgube svi, poziva se korutina koja definiše ponašanje za gubitak.
     */
    public void DecreaseNumberOfLives()
    {
        lifeCounter--;
        textForLives.text = lifeCounter.ToString();

        if (lifeCounter == 0)
        {
            StartCoroutine(gameOver());
        }
    }

    // Povećava broj života. Osvežava UI element.
    public void IncreaseNumberOfLives()
    {
        lifeCounter++;
        textForLives.text = lifeCounter.ToString();
    }

    /* 
     * Funkcija koja definiše gubitak u igri. 
     * Prikazuje se UI element za gubitak. Uklanja se igrač sa ekrana.
     * Čeka se tri sekunde, pre nego što se učita početna scena.
     */
    IEnumerator gameOver()
    {
        gameOverUI.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    /* 
     * Funkcija koja definiše pobedu u igri. 
     * Prikazuje se UI element za pobedu. Uklanja se igrač sa ekrana.
     * Čeka se tri sekunde, pre nego što se učita početna scena.
     */
    IEnumerator gameWin()
    {
        gameWinUI.SetActive(true);
        GameObject.FindWithTag("Player").SetActive(false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
