using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
}
