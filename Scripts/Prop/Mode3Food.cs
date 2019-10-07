using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mode3Food : Prop
{
    public AudioClip audioClip;
    public GameObject textobj;
    public int num;
    private void Start()
    {
        textobj.GetComponent<MeshRenderer>().sortingLayerName = "Food";
        textobj.GetComponent<MeshRenderer>().sortingOrder = 1;

        num = Random.Range(1, 10);
        textobj.GetComponent<TextMesh>().text = num.ToString();
        propType = PropType.Mode3Food;

        audioSource = GameObject.Find("PropSound").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnakeHead")
        {
            collision.gameObject.GetComponent<Body>().snake.Eat(this);
            PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
