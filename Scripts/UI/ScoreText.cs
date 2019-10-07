using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreText : MonoBehaviour
{
    GameManager gameManager;
    Text text;
    void Start()
    {
        gameManager = GameManager.gameManager;
        text = GetComponent<Text>();
        text.text = gameManager.score.ToString();
    }
    private void Update()
    {
        text.text = gameManager.score.ToString();
    }
}
