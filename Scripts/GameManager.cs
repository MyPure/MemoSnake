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
    public int currentLevel;

    public Dictionary<MusicSetting.MusicType, float> soundVolume = new Dictionary<MusicSetting.MusicType, float>() { { MusicSetting.MusicType.SoundMusic,1.0f },{ MusicSetting.MusicType.SoundEffect,1.0f } };
    public Dictionary<MusicSetting.MusicType, bool> soundMute = new Dictionary<MusicSetting.MusicType, bool>() { { MusicSetting.MusicType.SoundMusic, false }, { MusicSetting.MusicType.SoundEffect, false } };

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
    public void LoadLevel(int level)
    {
        currentMode = 2;
        currentLevel = level;
        SceneManager.LoadScene("Mode 2 Level " + level);
    }
    public void NextLevel()
    {
        if (currentLevel == 4)
        {
            BackToMainMenu();
        }
        else
        {
            LoadLevel(currentLevel + 1);
        }
    }
}
