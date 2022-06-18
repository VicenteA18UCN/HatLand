using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] coins;
    [SerializeField] GameObject[] potions;
    private Vector3 initialPlayerPosition;
    private Vector3 actualPlayerPosition;
    private int livesLeft;
    private int coinsAccumulator;

    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] TextMeshProUGUI  lifeText;
    
    private void OnEnable()
    {
        if(!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins",0);
        }
        if(!PlayerPrefs.HasKey("Lives"))
        {
            PlayerPrefs.SetInt("Lives",3);
        }
        this.livesLeft = PlayerPrefs.GetInt("Lives");
        this.coinsAccumulator = PlayerPrefs.GetInt("Coins");
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.coins = GameObject.FindGameObjectsWithTag("Coin");
        this.potions = GameObject.FindGameObjectsWithTag("Potion");
        this.lifeText.text = livesLeft.ToString();
        this.coinText.text = coinsAccumulator.ToString();
        return;
    }

    private void Start()
    {
        this.initialPlayerPosition = this.player.transform.position;
        this.actualPlayerPosition = this.player.transform.position;
    }

    private void Update() 
    {

        this.UpdateDeathCanvas(livesLeft);
        this.CoinObserver();
        this.PotionObserver();
        if(player.GetComponent<Player>().GetSaveGame())
        {
            SaveGame();
            player.GetComponent<Player>().SetSaveGame(false);
        }
        if(player.GetComponent<Player>().GetPlayerStatus())        
        {
            this.livesLeft--;
            this.player.transform.position = this.actualPlayerPosition;
            player.GetComponent<Player>().ResetPlayerStatus();
            this.UpdateDeathCanvas(this.livesLeft);
            if(livesLeft ==0)
            {
                LevelManager.LoadDeathMenu();
            }
            
        }
    }

    public void RestartGame()
    {
        this.player.GetComponent<Player>().ResetPlayerStatus();
        this.player.SetActive(false);
        this.RestartCoins();
        this.RestartPotions();
        this.initialPlayerPosition = this.player.transform.position;
        this.player.SetActive(true);
    }

    private void UpdateDeathCanvas(int amount)
    {
        livesLeft = amount;
        this.lifeText.text = livesLeft.ToString();
    }

    private void UpdateCoinsCanvas(int amount)
    {
        coinsAccumulator = amount;
        this.coinText.text = coinsAccumulator.ToString();
    }

    private void RestartCoins()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            this.coins[i].GetComponent<Coin>().ResetStatus();
            this.coins[i].SetActive(true);
        }
        this.coinsAccumulator = 0;
        this.UpdateCoinsCanvas(0);
        PlayerPrefs.SetInt("Coins",0);
    }

    private void CoinObserver()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            if(coins[i].GetComponent<Coin>().GetStatus())
            {
                coins[i].GetComponent<Coin>().ResetStatus();
                this.coins[i].SetActive(false);
                this.coinsAccumulator++;
                PlayerPrefs.SetInt("Coins",this.coinsAccumulator);
                this.UpdateCoinsCanvas(this.coinsAccumulator);
            } 
        }
    }

    private void RestartPotions()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            this.potions[i].GetComponent<Potion>().ResetStatus();
            this.potions[i].SetActive(true);
        }
    }

    private void PotionObserver()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if(this.potions[i].GetComponent<Potion>().GetStatus())
            {

                potions[i].GetComponent<Potion>().ResetStatus();
                //this.UpdateCoinsCanvas();
                this. potions[i].SetActive(false);
                this.livesLeft++;
                PlayerPrefs.SetInt("Lives",this.livesLeft);

            } 
        }
    }

    public void OnMouseOverNoButton()
    {
        return;
    }

    public void OnClickPlayGameButton()
    {
        LevelManager.LoadNextLevel();       
    }

    public void OnMouseOverPlayGameButton()
    {
         return; 
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Coins", this.coinsAccumulator);
        PlayerPrefs.SetInt("Lives", this.livesLeft);
        this.actualPlayerPosition = this.player.transform.position;
    }
}
