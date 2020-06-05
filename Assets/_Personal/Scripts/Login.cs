﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using JsonObject = PlayFab.Json.JsonObject;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using PlayFab.PfEditor.Json;


public class Login : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerUsernameTxt;
    [SerializeField] private TextMeshProUGUI playerEmailTxt;
    [SerializeField] private TextMeshProUGUI playerPasswordTxt;

    public GameObject loginPanel;
    public GameObject registerPanel;

    [SerializeField] private string playerUsername;
    [SerializeField] private string playerEmail;
    [SerializeField] private string playerPassword;

    private static Login instance;

    public void OnEnable() //Singleton Call when the object is enable
    {
        if (Login.instance == null) //if there isn't playfabController script in the scene
        {
            Login.instance = this; //this is our new playfabCOntroller script
        }
        else
            if (Login.instance != this) //or if there is another playfabController script in the scene
        {
            Destroy(this.gameObject); //Then destroy this one
        }
        DontDestroyOnLoad(this.gameObject); //When we switch scene the game object is not destroyed
    }
    public void Start()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("EMAIL")) //If I already logged
        {
            var request = new LoginWithEmailAddressRequest { Email = PlayerPrefs.GetString("EMAIL"), Password = PlayerPrefs.GetString("PASSWORD") }; 
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }

    }


    public void OnClickLogin() //Login button
    {
        var request = new LoginWithEmailAddressRequest { Email = playerEmail, Password = playerPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }




    private void OnLoginSuccess(LoginResult loginResult)
    {
        PlayerPrefs.SetString("EMAIL", playerEmail);
        PlayerPrefs.SetString("PASSWORD", playerPassword);
        //Disable the panel and go to game
        Debug.Log("logged");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("not logged");
    }

    public void GoToRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnClickRegister() //Register button
    {
        GetUserEmail();
        GetUserPassword();
        Debug.Log(playerEmail);
        Debug.Log(playerPassword);

        var registerRequest = new RegisterPlayFabUserRequest { Email = playerEmail, Password = playerPassword, RequireBothUsernameAndEmail = true, Username = "dev" };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayerPrefs.SetString("EMAIL", playerEmail);
        PlayerPrefs.SetString("PASSWORD", playerPassword);
        Debug.Log("registered");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Failed to register");
    }

    #region Get
    public void GetUserEmail()
    {        
        playerEmail = playerEmailTxt.text;
    }

    public void GetUserPassword()
    {
        playerPassword = playerPasswordTxt.text;
    }

    #endregion
}
