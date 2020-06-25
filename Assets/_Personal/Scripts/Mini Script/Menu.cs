using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image fadeGO;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeOut());
    }

    public void GoToNextLevel()
    {
        print("lol");
        StartCoroutine(fadein());
    }

    IEnumerator fadeOut()
    {
        for (float i = 0; i < 100; i++)
        {
            fadeGO.color = new Color(fadeGO.color.r, fadeGO.color.g, fadeGO.color.b, (1f - (i / 100f)));
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator fadein()
    {
        for (float i = 0; i < 100; i++)
        {
            fadeGO.color = new Color(fadeGO.color.r, fadeGO.color.g, fadeGO.color.b,( 0f + (i / 100f)));
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene(1);
    }
}
