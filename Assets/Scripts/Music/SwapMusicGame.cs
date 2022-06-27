using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapMusicGame : MonoBehaviour
{
    public bool isPause = false;
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            BGmusic.instance.GetComponent<AudioSource>().Stop();
            this.isPause = true;
        }
    }
}
