using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }   

    [SerializeField] private GameObject databaseLoginCanvas;
    [SerializeField] private TMP_Text gemsText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text bloodVailText;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OnClick_PlayBtn()
    {
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

    public void UpdateCurrencyText(int gems, int coins, int bloodVail)
    {
        gemsText.text = gems.ToString();
        coinsText.text = coins.ToString();
        bloodVailText.text = bloodVail.ToString();
    }
}
