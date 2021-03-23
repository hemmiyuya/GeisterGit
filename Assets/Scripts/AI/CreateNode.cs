using UnityEngine;

/// <summary>
/// ノード作成クラス
/// </summary>
public class CreateNode : MonoBehaviour
{
    /// <summary>
    /// 盤面管理
    /// </summary>
    private BoardManager _boardManager;
    /// <summary>
    /// 評価クラス
    /// </summary>
    private Evaluation _evaluation;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        _evaluation = GameObject.FindGameObjectWithTag("Evalution").GetComponent<Evaluation>();
    }

    /// <summary>
    /// 新しくノードを作る
    /// </summary>
    /// <returns></returns>
    public Node Create()
    {
        return new Node();
    }

    /// <summary>
    /// 与えられたノードから、想定しうるノードを作成する
    /// </summary>
    /// <param name="l_node"></param>
    public bool CreateChild(Node l_node)
    {
        int[] arrayCopy = l_node.g_nodeArrayBoard;
        //プレイヤーのターンで終わっているとき
        if (l_node.g_aiTurn == Turn.PLAYERTURN)
        {
            //プレイヤーの駒の数だけ繰り返す
            for (int i = 0; i < l_node.g_playerGeisterIndexs.Count; i++)
            {
                //上右下左の順番で
                for (int directionCount = 0; directionCount < 4; directionCount++)
                {
                    //指定したインデックスの駒を動かして、新しいノードとする
                    arrayCopy = _boardManager.DesignationDirectionCanMove(arrayCopy, l_node.g_playerGeisterIndexs[directionCount], (DirectionEnum)directionCount, l_node.g_aiTurn);
                    //動かせたら、与えられたノードの子ノードとして追加する
                    if (arrayCopy != l_node.g_nodeArrayBoard)
                    {
                        l_node.g_childrenNode.Add(Create()._nodeInit(arrayCopy, l_node, _boardManager.FindPlayerGeister(arrayCopy), _boardManager.FindEnemyGeister(arrayCopy), Turn.ENEMYTURN));
                    }
                }
            }
        }
        //敵のターンで終わっているとき
        else if(l_node.g_aiTurn == Turn.ENEMYTURN)
        {
            //上右下左の順番で
            for (int directionCount = 0; directionCount < 4; directionCount++)
            {
                //指定したインデックスの駒を動かして、新しいノードとする
                arrayCopy = _boardManager.DesignationDirectionCanMove(arrayCopy, l_node.g_playerGeisterIndexs[directionCount], (DirectionEnum)directionCount, l_node.g_aiTurn);
                //動かせたら、与えられたノードの子ノードとして追加する
                if (arrayCopy != l_node.g_nodeArrayBoard)
                {
                    l_node.g_childrenNode.Add(Create()._nodeInit(arrayCopy, l_node, _boardManager.FindPlayerGeister(arrayCopy), _boardManager.FindEnemyGeister(arrayCopy), Turn.PLAYERTURN));
                }
            }
        }

        l_node.g_evaluationPoint = _evaluation.EvaluationNow(l_node);
        if (l_node.g_playerGeisterIndexs.Count < 2 || l_node.g_enemyGeisterIndexs.Count < 2)
        {
            return true;
        }

        return false;

    }
}
