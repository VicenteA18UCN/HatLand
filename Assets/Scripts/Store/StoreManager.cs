using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] private SkinManager skinManager;
    [SerializeField] private Image selectedSkin;
    void Update()
    {
        this.CoinsCanvas();
        selectedSkin.sprite = skinManager.GetSelectedSkin().sprite;
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            CheatCoin();
        }
    }

    private void CoinsCanvas()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        this.coinText.text = coins.ToString();
    }

    private void CheatCoin()
    {
        PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins") + 50);
    }
}
