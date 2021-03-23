using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 選択数が一定以下のノードのプレイアウトを行う
/// </summary>
public class AIPLayOut : MonoBehaviour
{
    /// <summary>
    /// 盤面クラス
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// 勝利フラグ
    /// </summary>
    private bool[] _winFlag=new bool[2];

    /// <summary>
    /// どちらのターンか
    /// </summary>
    private int _turn = Turn.ENEMYTURN;

    /// <summary>
    /// どちらが勝ったか(Player=ture,Enemy=false)
    /// </summary>
    private bool _winPlayer=true;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
    }

    /// <summary>
    /// プレイアウト。プレイヤーと敵どちらかが勝利するまで、ランダムに進める
    /// </summary>
    public bool PlayOut(Node node)
    {
        //プレイアウト用の盤面
        int[] l_PlayOutArrayBoard = node.g_nodeArrayBoard;

        //勝つまで続ける
        while (!_winFlag[0])
        {
            //動かせる駒取得
            List<int> l_CanMoveIndex = _boardManager.CheckCanMoveAll(l_PlayOutArrayBoard,_turn);
            //一つランダムで取り出す
            int l_RandamIndex = l_CanMoveIndex[Random.Range(0, l_CanMoveIndex.Count)];
            //比較用配列
            int[] l_ComparisonArrayBoard=l_PlayOutArrayBoard;
            //ランダムな向きに動かす
            while (l_PlayOutArrayBoard != l_ComparisonArrayBoard)
            {
                _boardManager.DesignationDirectionCanMove(l_PlayOutArrayBoard, l_RandamIndex, (DirectionEnum)Random.Range(1, 4), _turn);
            }
            //ターン入れ替え
            _turn *= -1;
            _winFlag = _boardManager.ClearCheck(l_PlayOutArrayBoard);
        }

        return _winPlayer;
    }
}
