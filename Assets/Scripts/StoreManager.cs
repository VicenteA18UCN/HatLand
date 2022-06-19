using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    void Update()
    {
        this.CoinsCanvas();
    }

    private void CoinsCanvas()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        this.coinText.text = coins.ToString();
    }
}
