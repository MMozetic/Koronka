using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Kontolisanje puštanja muzike.
 */
public class MusicPlayerController : MonoBehaviour
{
    /* 
     * Klasa je statička, biće živa dokle god traje igrica.
     * Realizovana pomoću Singleton Class Pattern-a.
     */
    private static MusicPlayerController instance = null;
    private AudioSource musicSource;

    private int previousIndexScene = 0;
    private int currentIndexScene = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    /*
     * Pratiti indekse prethodne i trenutne scene.
     * Ako se desi promena sa prve scene na početnu scenu,
     * zaustaviti muziku i pustiti od početka trake.
     */
    void Update()
    {
        currentIndexScene = SceneManager.GetActiveScene().buildIndex;

        if(currentIndexScene == 0 && previousIndexScene == 1)
        {
            musicSource.Stop();
            musicSource.Play();
        }

        previousIndexScene = currentIndexScene;
    }

    // Na početku pustiti muziku.
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.Play();
    }
}
