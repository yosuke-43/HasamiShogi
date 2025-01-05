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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
