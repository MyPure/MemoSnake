using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : Button
{
    public void LoadMode(int mode)
    {
        gameManager.LoadMode(mode);
    }
}
