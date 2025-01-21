using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int Type;

    // インデックス番号
    public Vector2Int Pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // タイプの指定 0 == 1P, 1 == 2P
    public void SetUnitType(int type)
    {
        int dir = 1 - (type * 2);
        Vector3 ang = new Vector3(90 * dir, 0, 0);

        transform.eulerAngles = ang;
        
        Type = type;
    }

    // 選択された時
    public void SelectUnit(bool select = true)
    {
        Vector3 pos = transform.position;
        pos.y += 3;
        GetComponent<Rigidbody>().isKinematic = true;

        if (!select)
        {
            pos.y = 0.9f;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        transform.position = pos;
    }

    // 新しい場所へおく
    public void PutUnit(GameObject tile, Vector2Int tilepos)
    {
        // 非選択状態にする
        SelectUnit(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        Pos = tilepos; 

        // 新しい場所へ移動する
        Vector3 pos = tile.transform.position;

        pos.y += 0.9f;

        transform.position = pos;
    }
}
