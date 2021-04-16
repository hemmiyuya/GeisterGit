using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム進行管理
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 配列マネージャー
    /// </summary>
    private BoardManager _boradManager=default;

    private const int boardWidth=8;
    private const int boardHeight=6;

    /// <summary>
    /// プレイヤーマネージャーオブジェクト
    /// </summary>
    private GameObject _playerManagerObj;
    /// <summary>
    /// エネミーマネージャーオブジェクト
    /// </summary>
    private GameObject _enemyManagerObj;

    /// <summary>
    /// プレイヤーマネージャー
    /// </summary>
    private PlayerManeger _playerManager;
    /// <summary>
    /// エネミーマネージャー
    /// </summary>
    private EnemyManager _enemyManager;

    private GameObject _coinObj;

    /// <summary>
    /// 先行
    /// </summary>
    private GameObject _firstAttackPlayerObj;
    /// <summary>
    /// 後攻
    /// </summary>
    private GameObject _secondAttackPlayerObj;

    /// <summary>
    /// キャンバス
    /// </summary>
    private GameObject _canvasObj;

    /// <summary>
    /// プレイヤーの攻撃順表示テキスト
    /// </summary>
    private Text _playerAttackOrderText;

    /// <summary>
    /// 先攻のターンか後攻のターンか 1=先攻、2=後攻
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
        //配列生成
        _boradManager.CreateArray(boardWidth, boardHeight);

    }

    /// <summary>
    /// 行動順決定
    /// </summary>
    /// <param name="coinFace"></param>
    public IEnumerator SetAttackOrder(bool coinFace)
    {
        //表
        if (coinFace)
        {
            _firstAttackPlayerObj = _playerManagerObj;
            _secondAttackPlayerObj = _enemyManagerObj;
            Debug.Log("プレイヤーが先");
            _playerAttackOrderText.gameObject.SetActive(true);
            _playerAttackOrderText.text = "あなたは先攻です";
        }
        //裏
        else
        {
            _firstAttackPlayerObj = _enemyManagerObj;
            _secondAttackPlayerObj = _playerManagerObj;
            Debug.Log("敵が先");
            _playerAttackOrderText.gameObject.SetActive(true);
            _playerAttackOrderText.text = "あなたは後攻です";
        }

        yield return new WaitForSeconds(2.0f);
        _playerAttackOrderText.text = "";
        _coinObj.SetActive(false);
        //マス目配置
        _boradManager.SetPixel();
        _canvasObj.transform.GetChild(0).gameObject.SetActive(true);
        yield return StartCoroutine(_enemyManager.SetGeister());

        TurnStart();
        yield break;
    }

    /// <summary>
    /// ターン開始
    /// </summary>
    public void TurnStart()
    {
        _nowAttackOrder++;
        //一人目
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
        //二人目
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
        //二人とも終了時
        else if (_nowAttackOrder == (int)AttackOrder.End)
        {
            //ターン終了処理

            _nowAttackOrder = 0;
            TurnStart();
        }
    }
}
