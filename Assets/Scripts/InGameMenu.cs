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

    void Start()
    {
        PlayerController.Instance.OnGameFinished += PlayerController_OnGameFinished;

        completedContinueBtn.onClick.AddListener(BackToMenu);
        overContinueBtn.onClick.AddListener(BackToMenu);

        gameCompletedPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void PlayerController_OnGameFinished(object sender, bool gameCompeleted)
    {
        if (gameCompeleted)
            gameCompletedPanel.SetActive(true);
        else
            gameOverPanel.SetActive(true);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
