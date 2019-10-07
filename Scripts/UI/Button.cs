using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource buttonSound;
    private void Start()
    {
        gameManager = GameManager.gameManager;
        buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
    }
    public void DestroyNowCanvas()
    {
        GameObject nowObj = gameObject.transform.parent.gameObject;
        while (nowObj.GetComponent<Canvas>() == null)
            nowObj = nowObj.transform.parent.gameObject;
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void InstantiateObject(GameObject gameObject)
    {
        Instantiate(gameObject);
    }

    public void BackToMainMenu()
    {
        gameManager.BackToMainMenu();
    }

    public void Restart()
    {
        gameManager.Restart();
    }

    public void PlaySound()
    {
        buttonSound.Play();
    }
    public void NextLevel()
    {
        gameManager.NextLevel();
    }
    public void Pause()
    {
        gameManager.Pause();
    }
    public void UnPause()
    {
        gameManager.UnPause();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    
}
