using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    //Food,Mushroom,PoisonousGrass,Boom,Energy,Sheild
    public List<GameObject>[] props;
    public List<GameObject> propPrefabs;
    int[] propMax;
    Vector3 borden;//边界
    // Start is called before the first frame update
    void Start()
    {
        props = new List<GameObject>[6];
        for(int i = 0; i < 6; i++)
        {
            props[i] = new List<GameObject>();
        }
        propMax = new int[6] { 25, 5, 10, 10, 5, 5 };
        borden = transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].Count < propMax[i])
            {
                Debug.Log(i + " " + props[i].Count);
                int count = props[i].Count;
                for (int j = 0; j < propMax[i] - count; j++)
                {
                    float x = Random.Range(borden.x, -borden.x);
                    float y = Random.Range(-borden.y, borden.y);
                    props[i].Add(Instantiate(propPrefabs[i], new Vector3(x, y, 0), Quaternion.identity));
                }
            }
        }
    }
}
