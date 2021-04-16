using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Q�[���i�s�Ǘ�
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �z��}�l�[�W���[
    /// </summary>
    private BoardManager _boradManager=default;

    private const int boardWidth=8;
    private const int boardHeight=6;

    /// <summary>
    /// �v���C���[�}�l�[�W���[�I�u�W�F�N�g
    /// </summary>
    private GameObject _playerManagerObj;
    /// <summary>
    /// �G�l�~�[�}�l�[�W���[�I�u�W�F�N�g
    /// </summary>
    private GameObject _enemyManagerObj;

    /// <summary>
    /// �v���C���[�}�l�[�W���[
    /// </summary>
    private PlayerManeger _playerManager;
    /// <summary>
    /// �G�l�~�[�}�l�[�W���[
    /// </summary>
    private EnemyManager _enemyManager;

    private GameObject _coinObj;

    /// <summary>
    /// ��s
    /// </summary>
    private GameObject _firstAttackPlayerObj;
    /// <summary>
    /// ��U
    /// </summary>
    private GameObject _secondAttackPlayerObj;

    /// <summary>
    /// �L�����o�X
    /// </summary>
    private GameObject _canvasObj;

    /// <summary>
    /// �v���C���[�̍U�����\���e�L�X�g
    /// </summary>
    private Text _playerAttackOrderText;

    /// <summary>
    /// ��U�̃^�[������U�̃^�[���� 1=��U�A2=��U
    /// </summary>
    private int _nowAttackOrder=0;

    private enum AttackOrder
    {
        First=1,
        Second=2,
        End=3,
    }

    private void Awake()
    {
        _playerManagerObj = GameObject.FindGameObjectWithTag("PlayerManager");
        _playerManager = _playerManagerObj.GetComponent<PlayerManeger>();
        _enemyManagerObj = GameObject.FindGameObjectWithTag("EnemyManager");
        _enemyManager = _enemyManagerObj.GetComponent<EnemyManager>();
        _coinObj = GameObject.FindGameObjectWithTag("Coin");
        _canvasObj = GameObject.FindGameObjectWithTag("Canvas");
        _playerAttackOrderText = _canvasObj.transform.GetChild(2).GetComponent<Text>();
        _playerAttackOrderText.gameObject.SetActive(false);
        _boradManager =GameObject.FindWithTag("Board").GetComponent<BoardManager>();
    }

    private void Start()
    {
        _canvasObj.transform.GetChild(0).gameObject.SetActive(false);
        //�z�񐶐�
        _boradManager.CreateArray(boardWidth, boardHeight);

    }

    /// <summary>
    /// �s��������
    /// </summary>
    /// <param name="coinFace"></param>
    public IEnumerator SetAttackOrder(bool coinFace)
    {
        //�\
        if (coinFace)
        {
            _firstAttackPlayerObj = _playerManagerObj;
            _secondAttackPlayerObj = _enemyManagerObj;
            Debug.Log("�v���C���[����");
            _playerAttackOrderText.gameObject.SetActive(true);
            _playerAttackOrderText.text = "���Ȃ��͐�U�ł�";
        }
        //��
        else
        {
            _firstAttackPlayerObj = _enemyManagerObj;
            _secondAttackPlayerObj = _playerManagerObj;
            Debug.Log("�G����");
            _playerAttackOrderText.gameObject.SetActive(true);
            _playerAttackOrderText.text = "���Ȃ��͌�U�ł�";
        }

        yield return new WaitForSeconds(2.0f);
        _playerAttackOrderText.text = "";
        _coinObj.SetActive(false);
        //�}�X�ڔz�u
        _boradManager.SetPixel();
        _canvasObj.transform.GetChild(0).gameObject.SetActive(true);
        yield return StartCoroutine(_enemyManager.SetGeister());

        TurnStart();
        yield break;
    }

    /// <summary>
    /// �^�[���J�n
    /// </summary>
    public void TurnStart()
    {
        _nowAttackOrder++;
        //��l��
        if (_nowAttackOrder == (int)AttackOrder.First)
        {
            if (_firstAttackPlayerObj == _playerManagerObj)
            {
                _playerManager._myTurn = true;
            }
            else if (_firstAttackPlayerObj == _enemyManagerObj)
            {
                _enemyManager.EnemyTurn();
            }

        }
        //��l��
        else if (_nowAttackOrder == (int)AttackOrder.Second)
        {
            if (_secondAttackPlayerObj == _playerManagerObj)
            {
                _playerManager._myTurn = true;
            }
            else if (_secondAttackPlayerObj == _enemyManagerObj)
            {
                _enemyManager.EnemyTurn();
            }
        }
        //��l�Ƃ��I����
        else if (_nowAttackOrder == (int)AttackOrder.End)
        {
            //�^�[���I������

            _nowAttackOrder = 0;
            TurnStart();
        }
    }
}
