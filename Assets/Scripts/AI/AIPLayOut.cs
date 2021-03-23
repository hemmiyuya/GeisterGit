using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�𐔂����ȉ��̃m�[�h�̃v���C�A�E�g���s��
/// </summary>
public class AIPLayOut : MonoBehaviour
{
    /// <summary>
    /// �ՖʃN���X
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// �����t���O
    /// </summary>
    private bool[] _winFlag=new bool[2];

    /// <summary>
    /// �ǂ���̃^�[����
    /// </summary>
    private int _turn = Turn.ENEMYTURN;

    /// <summary>
    /// �ǂ��炪��������(Player=ture,Enemy=false)
    /// </summary>
    private bool _winPlayer=true;

    private void Awake()
    {
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
    }

    /// <summary>
    /// �v���C�A�E�g�B�v���C���[�ƓG�ǂ��炩����������܂ŁA�����_���ɐi�߂�
    /// </summary>
    public bool PlayOut(Node node)
    {
        //�v���C�A�E�g�p�̔Ֆ�
        int[] l_PlayOutArrayBoard = node.g_nodeArrayBoard;

        //���܂ő�����
        while (!_winFlag[0])
        {
            //���������擾
            List<int> l_CanMoveIndex = _boardManager.CheckCanMoveAll(l_PlayOutArrayBoard,_turn);
            //������_���Ŏ��o��
            int l_RandamIndex = l_CanMoveIndex[Random.Range(0, l_CanMoveIndex.Count)];
            //��r�p�z��
            int[] l_ComparisonArrayBoard=l_PlayOutArrayBoard;
            //�����_���Ȍ����ɓ�����
            while (l_PlayOutArrayBoard != l_ComparisonArrayBoard)
            {
                _boardManager.DesignationDirectionCanMove(l_PlayOutArrayBoard, l_RandamIndex, (DirectionEnum)Random.Range(1, 4), _turn);
            }
            //�^�[������ւ�
            _turn *= -1;
            _winFlag = _boardManager.ClearCheck(l_PlayOutArrayBoard);
        }

        return _winPlayer;
    }
}
