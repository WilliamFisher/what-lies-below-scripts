using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private int _mainSceneIndex;
    [SerializeField]
    private Text playButtonText;


    public void  PlayGame()
    {
        playButtonText.text = "Loading...";
        StartCoroutine(LoadMainScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadMainScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_mainSceneIndex, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
