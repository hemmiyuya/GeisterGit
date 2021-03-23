using System.Collections.Generic;
/// <summary>
/// ノード情報格納クラス
/// </summary>
public class Node 
{
    /// <summary>
    /// ゴールしてるかどうか
    /// </summary>
    public bool g_goal=default;
    /// <summary>
    /// ノード盤面
    /// </summary>
    public int[] g_nodeArrayBoard=null;
    /// <summary>
    /// 親ノード
    /// </summary>
    public Node g_parentNode=null;
    /// <summary>
    /// 子ノード
    /// </summary>
    public List<Node> g_childrenNode = null;
    /// <summary>
    /// 探索で選ばれた回数
    /// </summary>
    public int g_serachCount = default;
    /// <summary>
    /// プレイヤーが勝った数
    /// </summary>
    private int g_PlayerWinCount = default;
    /// <summary>
    /// 敵が勝った数
    /// </summary>
    private int g_EnemyWinCount = default;
    /// <summary>
    /// 勝率
    /// </summary>
    private float g_WinPercent=default;
    /// <summary>
    /// 評価点
    /// </summary>
    public float g_evaluationPoint=0;
    /// <summary>
    /// プレイヤーのガイスターのインデックス
    /// </summary>
    public List<int> g_playerGeisterIndexs=null;
    /// <summary>
    /// 敵のガイスターのインデックス
    /// </summary>
    public List<int> g_enemyGeisterIndexs=null;
    /// <summary>
    /// 最後に行動した人のターン プレイヤー=1,敵=-1
    /// </summary>
    public int g_aiTurn=0;

    /// <summary>
    /// ノード初期化
    /// </summary>
    /// <param name="l_ArrayBoard">現在の盤面</param>
    /// <param name="l_parentNode">親ノード</param>
    /// <param name="l_PlayerGeisterIndexs">プレイヤーのガイスターのインデックス一覧</param>
    /// <param name="l_EnemyGeisterIndexs">敵のガイスターのインデックス一覧</param>
    /// <returns>ノード返す</returns>
    public Node _nodeInit(int[] l_ArrayBoard,Node l_parentNode,List<int> l_PlayerGeisterIndexs,List<int> l_EnemyGeisterIndexs,int l_AITurn)
    {
        g_goal = false;
        g_nodeArrayBoard = l_ArrayBoard;
        g_parentNode = l_parentNode;
        g_childrenNode.Clear();
        g_serachCount = 0;
        g_evaluationPoint = 0;
        g_playerGeisterIndexs = l_PlayerGeisterIndexs;
        g_enemyGeisterIndexs = l_EnemyGeisterIndexs;
        g_aiTurn = l_AITurn;

        return this;
    }

}
