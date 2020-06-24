using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GameManager : Singleton<GameManager>
{
    [SerializeField] public GameAssets gameAssets;
    [HideInInspector] public int levelId;
    [SerializeField] KeyCode[] inputField;

    public enum ResourceType
    {
        None,
        Wood,
        Mouflu,
        Berry,
        Rock
    }
    #region States
    public enum GameState
    {
        Playing,
        Score
    }
    private GameState gameState;
    public GameState State
    {
        get { return gameState; }
        set { gameState = value; }
    }
    #endregion


    void Update()
    {
        for (int i =0; i < inputField.Length; i++)
        {
            if (Input.GetKeyDown(inputField[i]))
            {
                SceneManager.LoadScene(i);
            }
        }
    }

    void GoToNextLevel()
    {
        StartCoroutine(FadeInNextLevel());
    }

    IEnumerator FadeInNextLevel()
    {
        while (UIManager.Instance.fade.GetComponent<Image>().color.a < 1)
        {
            UIManager.Instance.fade.GetComponent<Image>().color = new Color(UIManager.Instance.fade.GetComponent<Image>().color.r, UIManager.Instance.fade.GetComponent<Image>().color.g,
                UIManager.Instance.fade.GetComponent<Image>().color.b, UIManager.Instance.fade.GetComponent<Image>().color.a + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    


    void ReloadSameLevel()
    {
        StartCoroutine(FadeInLevelReload());
    }

    IEnumerator FadeInLevelReload()
    {
        while (UIManager.Instance.fade.GetComponent<Image>().color.a < 1)
        {
            UIManager.Instance.fade.GetComponent<Image>().color = new Color(UIManager.Instance.fade.GetComponent<Image>().color.r, UIManager.Instance.fade.GetComponent<Image>().color.g,
                UIManager.Instance.fade.GetComponent<Image>().color.b, UIManager.Instance.fade.GetComponent<Image>().color.a + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator FadeOut()
    {
        while (UIManager.Instance.fade.GetComponent<Image>().color.a > 0)
        {
            UIManager.Instance.fade.GetComponent<Image>().color = new Color(UIManager.Instance.fade.GetComponent<Image>().color.r, UIManager.Instance.fade.GetComponent<Image>().color.g,
                UIManager.Instance.fade.GetComponent<Image>().color.b, UIManager.Instance.fade.GetComponent<Image>().color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);

        }
    }


    private void OnEnable()
    {
        ActionsButtons.ReloadTheSameLevel += ReloadSameLevel;
        ActionsButtons.GoToNextLevel += GoToNextLevel;
    }

    private void OnDisable()
    {
        ActionsButtons.ReloadTheSameLevel -= ReloadSameLevel;
        ActionsButtons.GoToNextLevel -= GoToNextLevel;

    }
}
