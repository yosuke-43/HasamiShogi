using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneDirector : MonoBehaviour
{
    public const int TILE_X = 9;
    public const int TILE_Y = 9;
    const int PLAYER_MAX = 2;

    // タイルとユニットのプレハブ
    public GameObject prefabTile;
    public GameObject prefabUnit;
    
    // UIオブジェクト
    GameObject textPlayer;
    GameObject textInfo;
    GameObject buttonApply;
    GameObject buttonCancel;

    // 内部データ
    GameObject[,] tiles, units;
    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクト取得
        textPlayer = GameObject.Find("TextPlayer");
        textInfo = GameObject.Find("TextInfo");
        buttonApply = GameObject.Find("ButtonApply");
        buttonCancel = GameObject.Find("ButtonCancel");

        textInfo.SetActive(false);
        buttonApply.SetActive(false);
        buttonCancel.SetActive(false);

        tiles = new GameObject[TILE_X, TILE_Y];
        units = new GameObject[TILE_X, TILE_Y];

        for (int i = 0; i < TILE_X; i++)
        {
            for (int j = 0; j < TILE_Y; j++)
            {
                // タイルとユニットのポジション
                float x = i - TILE_X / 2;
                float y = j - TILE_Y / 2;

                Vector3 pos = new Vector3(x, 0, y);

                // タイル作成
                GameObject tile = Instantiate(prefabTile, pos, Quaternion.identity);

                // ユニット作成
                GameObject unit = null;

                // 手前側の駒を作成
                if (j == 0)
                {
                    pos.y += 1;
                    unit = Instantiate(prefabUnit, pos, Quaternion.identity);
                }

                // 奥川の駒を作成
                if (j == TILE_Y -1)
                {
                    pos.y += 1;
                    unit = unit = Instantiate(prefabUnit, pos, Quaternion.identity);
                }

                // 内部データセット
                tiles[i, j] = tile;
                units[i, j] = unit;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
