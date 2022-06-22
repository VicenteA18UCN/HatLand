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
    }

    private void CoinsCanvas()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        this.coinText.text = coins.ToString();
    }
}
