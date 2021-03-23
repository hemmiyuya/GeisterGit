using System.Collections.Generic;
/// <summary>
/// �m�[�h���i�[�N���X
/// </summary>
public class Node 
{
    /// <summary>
    /// �S�[�����Ă邩�ǂ���
    /// </summary>
    public bool g_goal=default;
    /// <summary>
    /// �m�[�h�Ֆ�
    /// </summary>
    public int[] g_nodeArrayBoard=null;
    /// <summary>
    /// �e�m�[�h
    /// </summary>
    public Node g_parentNode=null;
    /// <summary>
    /// �q�m�[�h
    /// </summary>
    public List<Node> g_childrenNode = null;
    /// <summary>
    /// �T���őI�΂ꂽ��
    /// </summary>
    public int g_serachCount = default;
    /// <summary>
    /// �v���C���[����������
    /// </summary>
    private int g_PlayerWinCount = default;
    /// <summary>
    /// �G����������
    /// </summary>
    private int g_EnemyWinCount = default;
    /// <summary>
    /// ����
    /// </summary>
    private float g_WinPercent=default;
    /// <summary>
    /// �]���_
    /// </summary>
    public float g_evaluationPoint=0;
    /// <summary>
    /// �v���C���[�̃K�C�X�^�[�̃C���f�b�N�X
    /// </summary>
    public List<int> g_playerGeisterIndexs=null;
    /// <summary>
    /// �G�̃K�C�X�^�[�̃C���f�b�N�X
    /// </summary>
    public List<int> g_enemyGeisterIndexs=null;
    /// <summary>
    /// �Ō�ɍs�������l�̃^�[�� �v���C���[=1,�G=-1
    /// </summary>
    public int g_aiTurn=0;

    /// <summary>
    /// �m�[�h������
    /// </summary>
    /// <param name="l_ArrayBoard">���݂̔Ֆ�</param>
    /// <param name="l_parentNode">�e�m�[�h</param>
    /// <param name="l_PlayerGeisterIndexs">�v���C���[�̃K�C�X�^�[�̃C���f�b�N�X�ꗗ</param>
    /// <param name="l_EnemyGeisterIndexs">�G�̃K�C�X�^�[�̃C���f�b�N�X�ꗗ</param>
    /// <returns>�m�[�h�Ԃ�</returns>
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
