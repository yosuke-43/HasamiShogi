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

    // 選択したユニット
    GameObject selectUnit;

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

                    unit.GetComponent<UnitController>().SetUnitType(0);
                    unit.GetComponent<UnitController>().Pos = new Vector2Int(i, j);
                }

                // 奥側の駒を作成
                if (j == TILE_Y -1)
                {
                    pos.y += 1;
                    unit = unit = Instantiate(prefabUnit, pos, Quaternion.identity);

                    unit.GetComponent<UnitController>().SetUnitType(1);
                    unit.GetComponent<UnitController>().Pos = new Vector2Int(i, j);
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
        GameObject tile = null;
        GameObject unit = null;

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ユニットにも当たり判定があるので
            // ヒットした全てのオブジェクト情報を取得する
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                if(hit.transform.name.Contains("Tile"))
                {
                    tile = hit.transform.gameObject;
                    break;
                }
            }
        }

        // タイルが押されていなかったら何もしない
        if (tile == null) return;

        // 選んだタイルのポジションから配列の番号に戻す
        Vector2Int tilepos = new Vector2Int(
            (int)tile.transform.position.x + TILE_X / 2,
            (int)tile.transform.position.z + TILE_Y / 2);
        
        //ユニット
        unit = units[tilepos.x, tilepos.y];

        // 選択
        if (unit != null && selectUnit != unit)
        {
            // すでに選択状態
            if(selectUnit != null)
            {
                selectUnit.GetComponent<UnitController>().SelectUnit(false);
            }

            // 選択状態に
            selectUnit = unit;
            selectUnit.GetComponent<UnitController>().SelectUnit();
        }
        // 誰も乗っていないタイルが押されたら
        else if(selectUnit != null)
        {
            Vector2Int unitpos = selectUnit.GetComponent<UnitController>().Pos;
            
            // 新しい場所へ移動
            selectUnit.GetComponent<UnitController>().PutUnit(tile, tilepos);

            // 配列データ更新
            units[unitpos.x, unitpos.y] = null;
            units[tilepos.x, tilepos.y] = selectUnit;

            selectUnit = null;
        }
    }
}
