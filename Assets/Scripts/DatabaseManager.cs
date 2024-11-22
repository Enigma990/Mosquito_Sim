using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

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

    private void Start()
    {
        Login();
    }

    #region Login

    private void Login()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailed);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create");

        GetVirtualCurrencies();

        MainMenu.Instance.DatabaseLoginSuccessful();
    }

    private void OnLoginFailed(PlayFabError error)
    {
        Debug.Log("Error while login");
        Debug.Log(error.GenerateErrorReport());
    }

    #endregion

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }
    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        int coins = result.VirtualCurrency["CN"];
        int bloodVail = result.VirtualCurrency["BV"];

        MainMenu.Instance.UpdateCurrencyText(0, coins, bloodVail);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error while getting virtual currencies");
    }

    public void AddCurrency()
    {
        var coinsRequest = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "CN",
            Amount = GameManager.Instance.Coins
        };
        PlayFabClientAPI.AddUserVirtualCurrency(coinsRequest, OnUpdateVirtualCurrencySuccessful, OnError);

        var bloodVailRequest = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "BV",
            Amount = GameManager.Instance.BloodVail
        };
        PlayFabClientAPI.AddUserVirtualCurrency(bloodVailRequest, OnUpdateVirtualCurrencySuccessful, OnError);

    }

    private void OnUpdateVirtualCurrencySuccessful(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("updated currency successfully");
    }
}
