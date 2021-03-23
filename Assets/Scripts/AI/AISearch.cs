using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearch : MonoBehaviour
{
    /// <summary>
    /// �Ֆʔz��Ǘ��X�N���v�g
    /// </summary>
    private BoardManager _boardManager;
    /// <summary>
    /// AI�Ǘ��X�N���v�g
    /// </summary>
    private AIManager _aiManager;
    /// <summary>
    /// �m�[�h����N���X
    /// </summary>
    private CreateNode _createNode;
    /// <summary>
    /// �T�������
    /// </summary>
    private const int SEARCHCOUNT = 500;

    /// <summary>
    /// �T������[��
    /// </summary>
    private const int DEPTH = 5;

    /// <summary>
    /// �m�[�h�N���X
    /// </summary>
    private Node _node = new Node();

    /// <summary>
    /// �t�m�[�h�����������ǂ���
    /// </summary>
    private bool _findEndNodeFlag = false;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        _aiManager = GameObject.FindGameObjectWithTag("AIManager").GetComponent<AIManager>();
        _createNode = new CreateNode();
    }

    /// <summary>
    /// �T�����\�b�h
    /// </summary>
    public void Search()
    {
        //�e�m�[�h�����
        Node NowRootNode = _createNode.Create()._nodeInit(_aiManager._aiArrayBoard,null,_boardManager.FindPlayerGeister(_aiManager._aiArrayBoard),_boardManager.FindEnemyGeister(_aiManager._aiArrayBoard),Turn.PLAYERTURN);
        int NowDepthCount = 0;
        //�ŏ��̎q�m�[�h�Q�����
        _createNode.CreateChild(NowRootNode);
        //���ݕ]�����Ă���m�[�h
        List<Node> NowEvaluationNode = null;
        //�O��]�������m�[�h
        List<Node> OneBeforeEvaluationNode = null ;
        OneBeforeEvaluationNode.Add(NowRootNode);
        //�q�m�[�h�����I������m�[�h���ꎞ�ۊ�
        List<Node> KeepNode = null;

        //���߂�ꂽ�T���񐔌J��Ԃ�
        for (int i = 0; i < SEARCHCOUNT; i++)
        {
            //�t�m�[�h�܂ōs�����A�[�x5�ɓ��B����܂Ńm�[�h��i�߂�
            while (!_findEndNodeFlag && NowDepthCount <= DEPTH)
            {
                //�O��]�������m�[�h�̎q�m�[�h�����
                for (int h = 0; h < OneBeforeEvaluationNode.Count; h++)
                {
                    NowEvaluationNode.Add(OneBeforeEvaluationNode[h]);
                    for (int j = 0; j < NowEvaluationNode[j].g_childrenNode.Count; j++)
                    {
                        _findEndNodeFlag= _createNode.CreateChild(NowEvaluationNode[h].g_childrenNode[j]);
                        KeepNode.Add(NowEvaluationNode[h].g_childrenNode[j]);
                    }
                }
                //�L�[�v�m�[�h�ɕۊǂ������X�g�ŏ㏑��
                OneBeforeEvaluationNode = KeepNode;
                //������
                NowEvaluationNode = new List<Node>();
                KeepNode = new List<Node>();
                NowDepthCount++;
            }
        }
        

        //���������T��
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
