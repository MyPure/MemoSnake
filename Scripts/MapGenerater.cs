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

    int wallMax = 50;
    float wallGap = 3.0f;
    public List<GameObject> walls;
    public GameObject wallPrefab;
    static Vector2[] allDirection = new Vector2[8] { Vector2.up, new Vector2(1, 1), Vector2.right, new Vector2(1, -1), Vector2.down, new Vector2(-1, -1), Vector2.left, new Vector2(-1, 1) };

    // Start is called before the first frame update
    void Start()
    {
        props = new List<GameObject>[6];
        for (int i = 0; i < 6; i++)
        {
            props[i] = new List<GameObject>();
        }
        propMax = new int[6] { 25, 5, 10, 10, 5, 5 };
        borden = transform.GetChild(0).position;

        //生成墙
        walls = new List<GameObject>();
        while (walls.Count < wallMax)
        {
            float x = Random.Range(borden.x, -borden.x);
            float y = Random.Range(-borden.y, borden.y);
            int r2 = Random.Range(1, 7);//决定生成多墙的数量
            int r3 = Random.Range(0, 4);//方向
            for (int j = 0; j < r2; j++)
            {
                if (j == 0)
                {
                    if (haveWall(new Vector2(x, y), wallGap))
                    {
                        break;
                    }
                    else
                    {
                        walls.Add(Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity));
                    }
                }
                else
                {
                    if (haveWall(new Vector2(x, y) + allDirection[r3 * 2] * j, wallGap, r3 * 2 - 2))
                    {
                        break;
                    }
                    else
                    {
                        walls.Add(Instantiate(wallPrefab, new Vector2(x, y) + allDirection[r3 * 2] * j, Quaternion.identity));
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //生成道具和食物
        for (int i = 0; i < props.Length; i++)
        {
            if (props[i].Count < propMax[i])
            {
                Debug.Log(i + " " + props[i].Count);
                int count = props[i].Count;
                for (int j = 0; j < propMax[i] - count; j++)
                {
                    float x, y;
                    do
                    {
                        x = Random.Range(borden.x, -borden.x);
                        y = Random.Range(-borden.y, borden.y);
                    }
                    while (haveWall(new Vector2(x, y), 0.5f));
                    props[i].Add(Instantiate(propPrefabs[i], new Vector3(x, y, 0), Quaternion.identity));
                }
            }
        }

        
    }

    /// <summary>
    /// 检测各个方向上是否有墙
    /// </summary>
    /// <param name="pos">位置</param>
    /// <param name="range">半径</param>
    /// <returns>是否有墙</returns>
    bool haveWall(Vector2 pos, float range)
    {
        RaycastHit2D raycastHit2D;
        for (int i = 0; i < 8; i++)
        {
            raycastHit2D = Physics2D.Raycast(pos, allDirection[i], range);
            Debug.DrawLine(pos, pos + allDirection[i].normalized * range, Color.red);
            if (raycastHit2D.collider != null && raycastHit2D.collider.tag == "Wall")
            {
                return true;
            }

        }
        return false;
    }
    /// <summary>
    /// 检测各个方向上是否有墙
    /// </summary>
    /// <param name="pos">位置</param>
    /// <param name="range">半径</param>
    /// <param name="directionStart">开始的方向在allDirection的索引</param>
    /// <returns>是否有墙</returns>
    bool haveWall(Vector2 pos,float range,int directionStart)
    {
        RaycastHit2D raycastHit2D;
        if (directionStart < 0) directionStart += 8;
        for (int i = directionStart; i < directionStart+5; i++)
        {
            raycastHit2D = Physics2D.Raycast(pos, allDirection[i%8], range);
            //Debug.DrawLine(pos, pos + allDirection[i % 8].normalized * range, Color.red);
            if (raycastHit2D.collider != null && raycastHit2D.collider.tag == "Wall")
            {
                return true;
            }

        }
        return false;
    }
}
