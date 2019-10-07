using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpButton : Button
{
    public Sprite sourse;
    public void ChangeSprite(GameObject target)
    {
        target.GetComponent<Image>().sprite = sourse;
    }
}
