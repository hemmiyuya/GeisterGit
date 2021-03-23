using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ノードの評価を行うスクリプト
/// </summary>
public class Evaluation : MonoBehaviour
{
    /// <summary>
    /// 盤面管理クラス
    /// </summary>
    private BoardManager _boardManager;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
    }

    /// <summary>
    /// 評価
    /// </summary>
    /// <param name="l_Node">評価するノード</param>
    /// <returns></returns>
    public float EvaluationNow(Node l_Node)
    {
        float l_EvalutionPount = 0;
        int[] l_EvaArrayBoard = l_Node.g_nodeArrayBoard;
        /*
        //プレイヤーの時
        if (l_Node.g_aiTurn == Turn.PLAYERTURN)
        {
            //プレイヤーの駒の方が多い
            if (l_Node.g_playerGeisterIndexs.Count > l_Node.g_enemyGeisterIndexs.Count)
            {
                l_EvalutionPount+=0.5f;
            }
            //敵の駒の方が多い
            if (l_Node.g_enemyGeisterIndexs.Count > l_Node.g_playerGeisterIndexs.Count)
            {
                l_EvalutionPount -= 0.5f;
            }  
        }
        */
        
        //プレイヤーの数と敵の数の差分ポイント
        l_EvalutionPount += l_Node.g_enemyGeisterIndexs.Count - l_Node.g_playerGeisterIndexs.Count;

        //一番後ろの駒と前の駒の距離差分ポイント(前線を上げるとプラス
        l_EvalutionPount+=  _boardManager.ToPosition(l_Node.g_enemyGeisterIndexs.Last()).y- _boardManager.ToPosition(l_Node.g_enemyGeisterIndexs.First()).y;
        //プレイヤーの前線が高いとマイナス
        l_EvalutionPount-=  _boardManager.ToPosition(l_Node.g_playerGeisterIndexs.Last()).y- _boardManager.ToPosition(l_Node.g_playerGeisterIndexs.First()).y;
        //値をを返す      
        return l_EvalutionPount;



    }
}
