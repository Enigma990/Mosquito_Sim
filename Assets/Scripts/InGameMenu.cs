using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameCompletedPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Button completedContinueBtn;
    [SerializeField] private Button overContinueBtn;

    [Header("PowerUps")]
    [SerializeField] private Button armourButton;
    [SerializeField] private Button speedButton;
    [SerializeField] private Button stingMeterButton;
    [SerializeField] private Button bloodAmountButton;

    void Start()
    {
        PlayerController.Instance.OnGameFinished += PlayerController_OnGameFinished;

        completedContinueBtn.onClick.AddListener(BackToMenu);
        overContinueBtn.onClick.AddListener(BackToMenu);

        gameCompletedPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        armourButton.onClick.AddListener(() =>
        {
            PlayerController.Instance.UsePowerUp(PowerUpType.Armour, 2);
        });
        speedButton.onClick.AddListener(() =>
        {
            PlayerController.Instance.UsePowerUp(PowerUpType.Speed, 2);
        });
        stingMeterButton.onClick.AddListener(() =>
        {
            PlayerController.Instance.UsePowerUp(PowerUpType.StingMeter, 2);
        });
        bloodAmountButton.onClick.AddListener(() =>
        {
            PlayerController.Instance.UsePowerUp(PowerUpType.BloodAmount, 2);
        });

    }

    private void PlayerController_OnGameFinished(object sender, bool gameCompeleted)
    {
        if (gameCompeleted)
            gameCompletedPanel.SetActive(true);
        else
            gameOverPanel.SetActive(true);


        Camera.main.transform.SetParent(null);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
