using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearch : MonoBehaviour
{
    /// <summary>
    /// 盤面配列管理スクリプト
    /// </summary>
    private BoardManager _boardManager;
    /// <summary>
    /// AI管理スクリプト
    /// </summary>
    private AIManager _aiManager;
    /// <summary>
    /// ノード制作クラス
    /// </summary>
    private CreateNode _createNode;
    /// <summary>
    /// 探索する回数
    /// </summary>
    private const int SEARCHCOUNT = 500;

    /// <summary>
    /// 探索する深さ
    /// </summary>
    private const int DEPTH = 5;

    /// <summary>
    /// ノードクラス
    /// </summary>
    private Node _node = new Node();

    /// <summary>
    /// 葉ノードを見つけたかどうか
    /// </summary>
    private bool _findEndNodeFlag = false;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        _aiManager = GameObject.FindGameObjectWithTag("AIManager").GetComponent<AIManager>();
        _createNode = new CreateNode();
    }

    /// <summary>
    /// 探索メソッド
    /// </summary>
    public void Search()
    {
        //親ノードを作る
        Node NowRootNode = _createNode.Create()._nodeInit(_aiManager._aiArrayBoard,null,_boardManager.FindPlayerGeister(_aiManager._aiArrayBoard),_boardManager.FindEnemyGeister(_aiManager._aiArrayBoard),Turn.PLAYERTURN);
        int NowDepthCount = 0;
        //最初の子ノード群を作る
        _createNode.CreateChild(NowRootNode);
        //現在評価しているノード
        List<Node> NowEvaluationNode = null;
        //前回評価したノード
        List<Node> OneBeforeEvaluationNode = null ;
        OneBeforeEvaluationNode.Add(NowRootNode);
        //子ノードを作り終わったノードを一時保管
        List<Node> KeepNode = null;

        //決められた探索回数繰り返す
        for (int i = 0; i < SEARCHCOUNT; i++)
        {
            //葉ノードまで行くか、深度5に到達するまでノードを進める
            while (!_findEndNodeFlag && NowDepthCount <= DEPTH)
            {
                //前回評価したノードの子ノードを作る
                for (int h = 0; h < OneBeforeEvaluationNode.Count; h++)
                {
                    NowEvaluationNode.Add(OneBeforeEvaluationNode[h]);
                    for (int j = 0; j < NowEvaluationNode[j].g_childrenNode.Count; j++)
                    {
                        _findEndNodeFlag= _createNode.CreateChild(NowEvaluationNode[h].g_childrenNode[j]);
                        KeepNode.Add(NowEvaluationNode[h].g_childrenNode[j]);
                    }
                }
                //キープノードに保管したリストで上書き
                OneBeforeEvaluationNode = KeepNode;
                //初期化
                NowEvaluationNode = new List<Node>();
                KeepNode = new List<Node>();
                NowDepthCount++;
            }
        }
        

        //動かせる駒探す
        /*
        List<int> EnemyGeisters = _boardManager.CheckCanMoveAll(_aiManager._aiArrayBoard);
        List<int> CopyEnemyGeister = EnemyGeisters;
        for (int i = 0; i < 500; i++)
        {
            EnemyGeisters = CopyEnemyGeister;
        }    
        */
    }
}
