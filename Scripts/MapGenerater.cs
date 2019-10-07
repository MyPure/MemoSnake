using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerater : MonoBehaviour
{
    #region Mode1
    //Food,Mushroom,PoisonousGrass,Boom,Energy,Sheild
    public List<GameObject>[] props;
    public List<GameObject> propPrefabs;
    int[] propMax;
    Vector3 borden;//边界

    public float bornBorden = 4;//出生点范围

    int wallMax = 50;
    float wallGap = 3.0f;
    public List<GameObject> walls;
    public GameObject wallPrefab;
    static Vector2[] allDirection = new Vector2[8] { Vector2.up, new Vector2(1, 1), Vector2.right, new Vector2(1, -1), Vector2.down, new Vector2(-1, -1), Vector2.left, new Vector2(-1, 1) };
    #endregion

    #region Mode3
    public GameObject snake;
    public GameObject mode3FoodPrefab;
    public GameObject squarePrefab;
    public Sprite[] squareSprites;
    int regionLength = 48;
    public int currentRegion;//当前区域
    public int lastRegion;//最新更新区域
    int gap = 8;
    public int M3FoodCount = 9;
    public int SquareCount = 5;
    public Dictionary<int,List<GameObject>> regionObjects;
    #endregion
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
        borden = transform.GetChild(0).position;

        if (gameManager.currentMode == 1 || gameManager.currentMode == 2)
        {
            props = new List<GameObject>[6];
            for (int i = 0; i < 6; i++)
            {
                props[i] = new List<GameObject>();
            }
            propMax = new int[6] { 25, 2, 10, 10, 5, 5 };

            //生成墙
            walls = new List<GameObject>();
            GenerateWall();

            //生成道具
            GenerateProp(true);
        }
        else if(gameManager.currentMode == 3)
        {

            regionObjects = new Dictionary<int, List<GameObject>>();
            regionObjects.Add(0, new List<GameObject>());
            regionObjects.Add(1, new List<GameObject>());
            //在第零、一区域生成
            for (int x = 0, n = 0; x < regionLength * 2; x += gap, n++)
            {
                if (n % 2 == 0)
                {
                    GenerateM3Prop(x, M3FoodCount, PropType.Mode3Food, x / regionLength);
                }
                else
                {
                    GenerateM3Prop(x, SquareCount, PropType.Square, x / regionLength);
                }
            }
            lastRegion = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentMode == 1 || gameManager.currentMode == 2)
        {
            GenerateProp(false);
        }
        else if(gameManager.currentMode == 3)
        {
            currentRegion = (int)(snake.transform.position.x / regionLength);
            //在新区域生成
            if(currentRegion > lastRegion)
            {
                //生成新区域(current+1)
                regionObjects.Add(currentRegion + 1, new List<GameObject>());
                for (int x = (currentRegion + 1) * regionLength, n = 0; x < (currentRegion + 2) * regionLength; x += gap, n++)
                {
                    if (n % 2 == 0)
                    {
                        GenerateM3Prop(x, M3FoodCount, PropType.Mode3Food, currentRegion);
                    }
                    else
                    {
                        GenerateM3Prop(x, SquareCount, PropType.Square, currentRegion);
                    }
                }
                lastRegion = currentRegion;

                //删除旧区域所有物体
                if (currentRegion >= 2)
                {
                    DeleteRegion(currentRegion - 2);
                }
            }
            
            
        }
    }

    void GenerateProp(bool start)
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
                    bool s;
                    do
                    {
                        x = Random.Range(borden.x, -borden.x);
                        y = Random.Range(-borden.y, borden.y);
                        if (start)
                        {
                            s = Mathf.Abs(x) <= bornBorden && Mathf.Abs(y) <= bornBorden;
                        }
                        else
                        {
                            s = false;
                        }
                    }
                    while (haveWall(new Vector2(x, y), 0.5f) || s);
                    props[i].Add(Instantiate(propPrefabs[i], new Vector3(x, y, 0), Quaternion.identity));
                }
            }
        }
    }

    void GenerateWall()
    {
        while (walls.Count < wallMax)
        {
            float x, y;
            do
            {
                x = Random.Range(borden.x, -borden.x);
                y = Random.Range(-borden.y, borden.y);
            }
            while (Mathf.Abs(x) <= bornBorden && Mathf.Abs(y) <= bornBorden);
            int r2 = Random.Range(1, 7);//决定生成墙的数量
            int r3 = Random.Range(0, 4);//方向，0~3分别是上下左右
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
                    if (haveWall(new Vector2(x, y) + allDirection[r3 * 2] * j, wallGap, r3 * 2 - 2) || ((new Vector2(x, y) + allDirection[r3 * 2] * j).magnitude <= bornBorden))
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

    /// <summary>
    /// 生成带数字的食物
    /// </summary>
    /// <param name="posx">生成位置</param>
    /// <param name="count">生成数量，至少2个</param>
    void GenerateM3Prop(float posx,int count,PropType propType,int region)
    {
        if (count - 1 <= 0) return;
        for (int i = 0; i < count; i++){
            if(propType == PropType.Mode3Food)
            {
                regionObjects[region].Add(Instantiate(mode3FoodPrefab, new Vector3(posx, borden.y - i * 2 * borden.y / (count - 1), 0), Quaternion.identity));
            }
            else if(propType == PropType.Square)
            {
                GameObject s = Instantiate(squarePrefab, new Vector3(posx, borden.y - i * 2 * borden.y / (count - 1), 0), Quaternion.identity);
                s.GetComponent<SpriteRenderer>().sprite = squareSprites[Random.Range(0, squareSprites.Length)];
                regionObjects[region].Add(s);
            }
        }
    }

    void DeleteRegion(int region)
    {
        foreach(GameObject g in regionObjects[region])
        {
            Destroy(g);
        }
        regionObjects.Remove(region);
    }
}
