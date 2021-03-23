using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �m�[�h�̕]�����s���X�N���v�g
/// </summary>
public class Evaluation : MonoBehaviour
{
    /// <summary>
    /// �ՖʊǗ��N���X
    /// </summary>
    private BoardManager _boardManager;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
    }

    /// <summary>
    /// �]��
    /// </summary>
    /// <param name="l_Node">�]������m�[�h</param>
    /// <returns></returns>
    public float EvaluationNow(Node l_Node)
    {
        float l_EvalutionPount = 0;
        int[] l_EvaArrayBoard = l_Node.g_nodeArrayBoard;
        /*
        //�v���C���[�̎�
        if (l_Node.g_aiTurn == Turn.PLAYERTURN)
        {
            //�v���C���[�̋�̕�������
            if (l_Node.g_playerGeisterIndexs.Count > l_Node.g_enemyGeisterIndexs.Count)
            {
                l_EvalutionPount+=0.5f;
            }
            //�G�̋�̕�������
            if (l_Node.g_enemyGeisterIndexs.Count > l_Node.g_playerGeisterIndexs.Count)
            {
                l_EvalutionPount -= 0.5f;
            }  
        }
        */
        
        //�v���C���[�̐��ƓG�̐��̍����|�C���g
        l_EvalutionPount += l_Node.g_enemyGeisterIndexs.Count - l_Node.g_playerGeisterIndexs.Count;

        //��Ԍ��̋�ƑO�̋�̋��������|�C���g(�O�����グ��ƃv���X
        l_EvalutionPount+=  _boardManager.ToPosition(l_Node.g_enemyGeisterIndexs.Last()).y- _boardManager.ToPosition(l_Node.g_enemyGeisterIndexs.First()).y;
        //�v���C���[�̑O���������ƃ}�C�i�X
        l_EvalutionPount-=  _boardManager.ToPosition(l_Node.g_playerGeisterIndexs.Last()).y- _boardManager.ToPosition(l_Node.g_playerGeisterIndexs.First()).y;
        //�l����Ԃ�      
        return l_EvalutionPount;



    }
}
