using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCinematic : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(FinishGame),30f);
    }

    void Update()
    {
        this.EscapeCinematic();
    }

    void FinishGame()
    {
        SceneManager.LoadScene("Menu");
    }

    void EscapeCinematic()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CancelInvoke(nameof(FinishGame));
            FinishGame();
        }
    }
}
