using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapMusic : MonoBehaviour
{
    public bool isPause = false;
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            BGmusic.instance.GetComponent<AudioSource>().Stop();
            this.isPause = true;
        }

        if (SceneManager.GetActiveScene().name == "Store")
        {
            BGmusic.instance.GetComponent<AudioSource>().Stop();
            this.isPause = true;
        }
        
        if(SceneManager.GetActiveScene().name == "Menu" && this.isPause)
        {
            BGmusic.instance.GetComponent<AudioSource>().Play();
            this.isPause = false;
        }
    }
}
