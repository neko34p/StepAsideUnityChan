using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject conePrefab;
    /** スタート地点 */
    private int startPos = -160;
    /** ゴール地点 */
    private int goalPos = 120;
    /** アイテムを出すx方向の範囲 */
    private float posRange = 3.4f;
    /** Unityちゃん位置、監視用 */
    private GameObject unitychan;
    /** 作成済み地点 */
    private int createdPos;
    /** 作成エリア */
    private int createZRange = 15;

    // Use this for initialization
    void Start () {
        this.unitychan = GameObject.Find("unitychan");
        createdPos = startPos;
	}

    // Update is called once per frame
    void Update () {
        if (this.needCreation())
        {
            this.CreateItem();
        }

    }

    /** 作成が必要か判定 */
    bool needCreation()
    {
        if (createdPos + createZRange >= goalPos)
        {
            return false; //ゴールまでたどり着いているので不要
        }
        if (this.unitychan.transform.position.z + 45 > createdPos)
        {
            return true; //現在の位置＋45より作成済み地点が小さかったらtrue
        }
        return false;
    }

    /** 
     * 15メートルぶんのアイテム生成を行う。
     * Lessonの「for (int i = startPos; i < goalPos; i+=15)」の中を移植。
     */
    void CreateItem()
    {
        // 作成済み地点を設定
        int i = createdPos;

        // どのアイテムを出すのかをランダムに設定
        int num = Random.Range(0, 10);
        if (num <= 1)
        {
            // コーンをx軸方向に一直線に生成
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab) as GameObject;
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
            }
        }
        else
        {
            // レーンごとにアイテムを生成
            for (int j = -1; j < 2; j++)
            {
                // アイテムの種類を決める
                int item = Random.Range(1, 11);
                // アイテムを置くZ座標のオフセットをランダムに設定
                int offsetZ = Random.Range(-5, 6);

                // 60%　コイン配置　:30%　車配置　:10%　何もなし
                if (1 <= item && item <= 6)
                {
                    // コインを生成
                    GameObject coin = Instantiate(coinPrefab) as GameObject;
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                }
                else if (7 <= item && item <= 9)
                {
                    // 車を生成
                    GameObject car = Instantiate(carPrefab) as GameObject;
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                }
            }
        }

        // 作成済み地点を加算
        createdPos += createZRange;
    }
}
