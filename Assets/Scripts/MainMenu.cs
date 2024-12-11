using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }   

    [SerializeField] private GameObject databaseLoginCanvas;
    [SerializeField] private TMP_Text gemsText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text bloodVailText;

    [SerializeField] private Button stingerUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button stealthUpgradeButton;

    [SerializeField] private TMP_Text stingerText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text stealthText;

    [SerializeField] private GameObject mosquitoSelectCanvas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        mosquitoSelectCanvas.SetActive(false);

        stingerUpgradeButton.onClick.AddListener(() =>
        {
            StatsManager.Instance.UpgradeStingMeter();
            UpdateText();
        });

        speedUpgradeButton.onClick.AddListener(() =>
        {
            StatsManager.Instance.UpgradeSpeed();
            UpdateText();
        });

        stealthUpgradeButton.onClick.AddListener(() =>
        {
            StatsManager.Instance.UpgradeArmour();
            UpdateText();
        });

        UpdateText();
    }

    private void OnDestroy()
    {
        stingerUpgradeButton.onClick.RemoveAllListeners();
        speedUpgradeButton.onClick.RemoveAllListeners();
        stealthUpgradeButton.onClick.RemoveAllListeners();

    }

    public void OnClick_PlayBtn(int mosquitoType)
    {
        switch(mosquitoType)
        {
            case 0:
                //Queen
                StatsManager.Instance.SelectedMosquito(MosquitoType.Queen);
                break;

            case 1:
                StatsManager.Instance.SelectedMosquito(MosquitoType.Assassin);
                //Assassin
                break;

            case 2:
                //Warrior
                StatsManager.Instance.SelectedMosquito(MosquitoType.Warrior);
                break;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DatabaseLoginSuccessful()
    {
        databaseLoginCanvas.SetActive(true);

        Invoke(nameof(CloseDatabaseLoginPanel), 1f);
    }

    private void CloseDatabaseLoginPanel()
    {
        databaseLoginCanvas.SetActive(false);
    }

    private void UpdateText()
    {
        stingerText.text = "Stinger" + Environment.NewLine + "Lvl" + (StatsManager.Instance.GetStingMeter() + 1);
        speedText.text = "Speed" + Environment.NewLine + "Lvl" + (StatsManager.Instance.GetSpeed() + 1);
        stealthText.text = "Stealth" + Environment.NewLine + "Lvl" + (StatsManager.Instance.GetArmourAmount() + 1);
    }

    public void UpdateCurrencyText(int gems, int coins, int bloodVail)
    {
        gemsText.text = gems.ToString();
        coinsText.text = coins.ToString();
        bloodVailText.text = bloodVail.ToString();
    }

}
