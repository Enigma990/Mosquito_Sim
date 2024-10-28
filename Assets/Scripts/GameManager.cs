using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] private TMP_Text bloodVailText;

    public int Coins { get; private set; }
    public int BloodVail { get; private set; }

    public event EventHandler<OnMiniGameEventArgs> OnMiniGameCompleted;

    public class OnMiniGameEventArgs : EventArgs
    {
        public bool hasSucceeded;
        public int amount;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        bloodVailText.text = BloodVail.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddCoin(int amount)
    {
        Coins += amount;
    }

    private void AddBloodVail(int amount)
    {
        BloodVail += amount;

        bloodVailText.text = BloodVail.ToString();
    }

    public void MiniGameCompleted(int amount)
    {
        AddBloodVail(amount);
    }

}
