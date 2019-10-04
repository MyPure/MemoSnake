using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool first = false;
    public int score;
    public Snake snake;
    public int currentMode;

    void Start()
    {
        DontDestroyOnLoad(this);
        if (!first)
        {
            first = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMode(int mode)
    {
        currentMode = mode;
        SceneManager.LoadScene("Mode " + mode);
    }
}
