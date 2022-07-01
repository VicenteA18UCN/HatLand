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
    [SerializeField] GameObject[] feathers;
    [SerializeField] GameObject imageFeather;
    [SerializeField] GameObject key;
    public static bool hasKey;
    private Vector3 initialPlayerPosition;
    public Vector3 actualPlayerPosition;
    private bool isGettedFeather;
    private int livesLeft;
    private int coinsAccumulator;
    float currentTime = 0f;
    float startingTime = 10f;

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI  lifeText;
    private bool timeDown;
    public static bool Continue;

    
    [SerializeField] private AudioSource collettionSoundEffect;

    private void OnEnable()
    {
        this.PlayerComponents();
        if (Continue)
        {
            Continue = false;
            ContinuePress();
        }
    }

    private void Start()
    {
        this.PlayerPosition();
    }

    private void Update() 
    {
        this.UpdateDeathCanvas(livesLeft);
        this.CoinObserver();
        this.PotionObserver();
        this.FeatherObserver();
        this.KeyObserver();
        if(player.GetComponent<Player>().GetSaveGame())
        {
            SaveGame();
            player.GetComponent<Player>().SetSaveGame(false);
        }
        if(player.GetComponent<Player>().GetPlayerStatus())        
        {
            this.livesLeft--;
            CancelInvoke(nameof(CallStopImageFeather));
            CancelInvoke(nameof(CallStopPowerFeather));
            this.CallStopPowerFeather();
            this.CallStopImageFeather();
            currentTime = 0;
            this.player.transform.position = this.actualPlayerPosition;
            RestartFeather();
            player.GetComponent<Player>().ResetPlayerStatus();
            this.UpdateDeathCanvas(this.livesLeft);
            if(livesLeft ==0)
            RestartKey();
            {
                LevelManager.LoadDeathMenu();
            }
            
        }
        if(timeDown)
        {
            this.UpdateTimeCanvas();
        }
    }

    public void PlayerComponents()
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
        this.feathers = GameObject.FindGameObjectsWithTag("Feather");
        this.key = GameObject.FindGameObjectWithTag("Key");
        this.lifeText.text = livesLeft.ToString();
        this.coinText.text = coinsAccumulator.ToString();
        this.countdownText.text = "";
        this.currentTime = startingTime;
        hasKey = false;
    }

    public void PlayerPosition()
    {
        this.initialPlayerPosition = this.player.transform.position;
        this.actualPlayerPosition = this.player.transform.position;
    }
    public void RestartGame()
    {
        this.player.GetComponent<Player>().ResetPlayerStatus();
        this.player.SetActive(false);
        this.RestartCoins();
        this.RestartPotions();
        this.RestartFeather();
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

    private void UpdateTimeCanvas()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            this.countdownText.text = "";
            currentTime = startingTime;
            this.timeDown = false;
        }
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
                collettionSoundEffect.Play();
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
                collettionSoundEffect.Play();
                potions[i].GetComponent<Potion>().ResetStatus();
                //this.UpdateCoinsCanvas();
                this. potions[i].SetActive(false);
                this.livesLeft++;
                PlayerPrefs.SetInt("Lives",this.livesLeft);
            } 
        }
    }

    private void RestartFeather()
    {
        for (int i = 0; i < feathers.Length; i++)
        {
            this.feathers[i].GetComponent<Feather>().ResetStatus();
            this.feathers[i].SetActive(true);
        }
    }
    private void FeatherObserver()
    {
        for (int i = 0; i < feathers.Length; i++)
        {
            if (this.feathers[i].GetComponent<Feather>().GetStatus())
            {
                collettionSoundEffect.Play();
                if (this.isGettedFeather)
                {
                    CancelInvoke(nameof(CallStopImageFeather));
                    CancelInvoke(nameof(CallStopPowerFeather));
                    Invoke(nameof(CallStopPowerFeather),10f);
                    Invoke(nameof(CallStopImageFeather),10f);
                }
                feathers[i].GetComponent<Feather>().ResetStatus();
                this.feathers[i].SetActive(false);
                this.currentTime = this.startingTime;
                this.timeDown = true;
                this.isGettedFeather = true;
                imageFeather.SetActive(true);
                player.GetComponent<Player>().StartPowerFeather();
                Invoke(nameof(CallStopPowerFeather),10f);
                Invoke(nameof(CallStopImageFeather),10f);
            }
        }
    }

    public void KeyObserver()
    {
        if(this.key.GetComponent<Key>().GetStatus())
        {
            collettionSoundEffect.Play();
            this.key.GetComponent<Key>().ResetStatus();
            this.key.SetActive(false);
            hasKey = true;
        }
    }

    public void RestartKey()
    {
        this.key.GetComponent<Key>().ResetStatus();
        this.key.SetActive(true);
        hasKey = false;
    }

    public void CallStopPowerFeather()
    {
        player.GetComponent<Player>().StopPowerFeather();
        this.isGettedFeather = false;
    }

    public void CallStopImageFeather()
    {
        imageFeather.SetActive(false);
        this.isGettedFeather = false;
    }


    public void OnClickPlayGameButton()
    {
        LevelManager.LoadFirstLevel();       
    }


    public void SaveGame()
    {
        print("save game");
        actualPlayerPosition = player.transform.position;
        PlayerPrefs.SetInt("Coins", this.coinsAccumulator);
        PlayerPrefs.SetInt("Lives", this.livesLeft);
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("X", this.player.transform.position.x);
        PlayerPrefs.SetFloat("Y", this.player.transform.position.y);
        PlayerPrefs.SetFloat("Z", this.player.transform.position.z);
        
    }

    public void ContinuePress()
    {
        player.transform.position = new Vector3 (PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));

    }
}
