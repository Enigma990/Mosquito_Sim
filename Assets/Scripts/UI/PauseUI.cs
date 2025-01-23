using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backButton;

    private void Start()
    {
        homeButton.onClick.AddListener(() =>
        {
            HomeButtonClicked();
        });

        backButton.onClick.AddListener(() =>
        {
            BackButtonClicked();
        });
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    private void HomeButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void BackButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
