using UnityEngine;

/// <summary>
/// �m�[�h�쐬�N���X
/// </summary>
public class CreateNode : MonoBehaviour
{
    /// <summary>
    /// �ՖʊǗ�
    /// </summary>
    private BoardManager _boardManager;
    /// <summary>
    /// �]���N���X
    /// </summary>
    private Evaluation _evaluation;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("BoardManager").GetComponent<BoardManager>();
        _evaluation = GameObject.FindGameObjectWithTag("Evalution").GetComponent<Evaluation>();
    }

    /// <summary>
    /// �V�����m�[�h�����
    /// </summary>
    /// <returns></returns>
    public Node Create()
    {
        return new Node();
    }

    /// <summary>
    /// �^����ꂽ�m�[�h����A�z�肵����m�[�h���쐬����
    /// </summary>
    /// <param name="l_node"></param>
    public bool CreateChild(Node l_node)
    {
        int[] arrayCopy = l_node.g_nodeArrayBoard;
        //�v���C���[�̃^�[���ŏI����Ă���Ƃ�
        if (l_node.g_aiTurn == Turn.PLAYERTURN)
        {
            //�v���C���[�̋�̐������J��Ԃ�
            for (int i = 0; i < l_node.g_playerGeisterIndexs.Count; i++)
            {
                //��E�����̏��Ԃ�
                for (int directionCount = 0; directionCount < 4; directionCount++)
                {
                    //�w�肵���C���f�b�N�X�̋�𓮂����āA�V�����m�[�h�Ƃ���
                    arrayCopy = _boardManager.DesignationDirectionCanMove(arrayCopy, l_node.g_playerGeisterIndexs[directionCount], (DirectionEnum)directionCount, l_node.g_aiTurn);
                    //����������A�^����ꂽ�m�[�h�̎q�m�[�h�Ƃ��Ēǉ�����
                    if (arrayCopy != l_node.g_nodeArrayBoard)
                    {
                        l_node.g_childrenNode.Add(Create()._nodeInit(arrayCopy, l_node, _boardManager.FindPlayerGeister(arrayCopy), _boardManager.FindEnemyGeister(arrayCopy), Turn.ENEMYTURN));
                    }
                }
            }
        }
        //�G�̃^�[���ŏI����Ă���Ƃ�
        else if(l_node.g_aiTurn == Turn.ENEMYTURN)
        {
            //��E�����̏��Ԃ�
            for (int directionCount = 0; directionCount < 4; directionCount++)
            {
                //�w�肵���C���f�b�N�X�̋�𓮂����āA�V�����m�[�h�Ƃ���
                arrayCopy = _boardManager.DesignationDirectionCanMove(arrayCopy, l_node.g_playerGeisterIndexs[directionCount], (DirectionEnum)directionCount, l_node.g_aiTurn);
                //����������A�^����ꂽ�m�[�h�̎q�m�[�h�Ƃ��Ēǉ�����
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
