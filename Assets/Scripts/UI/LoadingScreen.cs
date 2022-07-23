using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    int LevelIndex = 0;

    public int _LevelIndex
    {
        set
        {
            LevelIndex = value;
            LoadLevel();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(LevelIndex);
        yield return new WaitForEndOfFrame();

    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
        StartCoroutine(LoadAsyncOperation());

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
