using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵管理スクリプト
/// </summary>
public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// 配列オブジェクト
    /// </summary>
    private GameObject _boardObj;

    /// <summary>
    /// 配列管理スクリプト
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// AIの管理
    /// </summary>
    private AIManager _aiManager;

    /// <summary>
    /// AIのガイスター動かすスクリプト
    /// </summary>
    private AIGeisterMove _aiMove;

    /// <summary>
    /// 敵のガイスター
    /// </summary>
    private GameObject _enemyGeister;

    /// <summary>
    /// 残りガイスター
    /// </summary>
    private int _residueGeister = 8;

    /// <summary>
    /// 青ガイスター残り
    /// </summary>
    private int _residueBlueGeister = 4;
    /// <summary>
    /// 赤ガイスター残り
    /// </summary>
    private int _residueRedGeister = 4;

    /// <summary>
    /// ガイスターの色青
    /// </summary>
    private const int Blue = 0;
    /// <summary>
    /// ガイスターの色赤
    /// </summary>
    private const int Red = 1;

    private void Awake()
    {
        _aiManager = GameObject.FindGameObjectWithTag("AIManager").GetComponent<AIManager>();
        _boardObj = GameObject.FindGameObjectWithTag("Board");
        _boardManager = _boardObj.GetComponent<BoardManager>();
    }

    /// <summary>
    /// 敵のターン
    /// </summary>
    public void EnemyTurn()
    {
        //敵の行動処理
        //配列のコピー
        _aiManager.SetNowBoard(_boardManager.ArrayBoard);


        //ターン終了

    }

    /// <summary>
    /// ガイスター取られたとき
    /// </summary>
    public void ResidueCountDown(GameObject geister)
    {
        if (geister.GetComponent<Geister>()._color == Blue)
        {
            _residueBlueGeister--;

        }
        else if (geister.GetComponent<Geister>()._color == Red)
        {
            _residueRedGeister--;
        }

        _residueGeister--;

        if (_residueBlueGeister == 0)
        {
            //プレイヤー勝利
        }
        if (_residueRedGeister == 0)
        {
            //敵勝利
        }

    }

    /// <summary>
    /// ガイスター置く
    /// </summary>
    public IEnumerator SetGeister()
    {
        Debug.Log("セットガイスター");
        //配置する配列番号の範囲
        int minValue1 = 25;
        int maxValue1 = 28;
        List<int> randomList1 = new List<int>();

        int minValue2 = 31;
        int maxValue2 = 34;
        List<int> randomList2 = new List<int>();

        int colorMinValue = 0;
        int colorMaxValue = 7;
        List<int> randomColorList = new List<int>();

        //一段目リスト
        for(int i = minValue1; i <= maxValue1; i++)
        {
            randomList1.Add(i);
        }
        //二段目リスト
        for(int i = minValue2; i <= maxValue2; i++)
        {
            randomList2.Add(i);
        }
        //色リスト
        for(int i = colorMinValue; i <= colorMaxValue; i++)
        {
            if (i <= 3)
            {
                randomColorList.Add(1);
            }
            else
            {
                randomColorList.Add(2);
            }
        }

        //重複しないように配置する
        while (randomList2.Count > 0)
        {
            //乱数取得
            int randomIndex = Random.Range(0, randomList1.Count);
            int randomValue = randomList1[randomIndex];
            randomList1.RemoveAt(randomIndex);
            //色決定
            int randomColorIndex = Random.Range(0, randomColorList.Count);
            int randomColorValue = randomColorList[randomColorIndex];
            randomColorList.RemoveAt(randomColorIndex);
            //座標変換
            Vector3 randomvector3 = _boardManager.ToPosition(randomValue);
            if (randomColorValue == 1) 
            { 
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterBlue("Enemy");
            }
            else if (randomColorValue == 2)
            {
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterRed("Enemy");
            }
            
            //二段目も同じように
            randomIndex= Random.Range(0, randomList2.Count);
            randomValue = randomList2[randomIndex];
            randomList2.RemoveAt(randomIndex);
            randomColorIndex = Random.Range(0, randomColorList.Count);
            randomColorValue = randomColorList[randomColorIndex];
            randomColorList.RemoveAt(randomColorIndex);

            randomvector3 = _boardManager.ToPosition(randomValue);
            if (randomColorValue == 1)
            {
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterBlue("Enemy");
            }
            else if (randomColorValue == 2)
            {
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterRed("Enemy");
            }

            yield return new WaitForFixedUpdate();
        }
        yield break;
        
    }

    /// <summary>
    /// ゴールする
    /// </summary>
    public void Goal()
    {

    }
}
