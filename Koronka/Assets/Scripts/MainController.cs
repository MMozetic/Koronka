using UnityEngine;
using UnityEngine.SceneManagement;

// Skripta koja se koristi za kontrolu menija.
public class MainController : MonoBehaviour
{
    // Prilikom pokretanja igre, učitati narednu scenu odnosno nivo.
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Prilikom izlaska iz igre, ubiti instancu aplikacije.
    public void ExitGame()
    {
        Application.Quit();
    }

    // Prilikom resetovanja igre, učitati početnu scenu sa indeksom 0.
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}

