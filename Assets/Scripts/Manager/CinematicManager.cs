using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(PlayGame),17f);
    }

    void Update()
    {
        this.EscapeCinematic();
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Nivel1");
    }

    void EscapeCinematic()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PlayGame();
        }
    }
}
