using System.Collections;
using System;
using UnityEngine;
using StateManager;
using UnityEngine.UI;

/// <summary>
/// プレイヤー管理スクリプト
/// </summary>
public class PlayerManeger : MonoBehaviour
{
    /// <summary>
    /// タッチ管理スクリプト
    /// </summary>
    private TouchManager _touchManager=default;

    /// <summary>
    /// プレイヤーのガイスターオブジェクト配列
    /// </summary>
    private GameObject[] _playerGeister=new GameObject[8];

    /// <summary>
    /// ガイスターつかんでるときに表示するやつ
    /// </summary>
    private GameObject _geisterPrefab = default;

    /// <summary>
    /// つかまれてるガイスター
    /// </summary>
    private GameObject _chachedGeister=default;

    /// <summary>
    /// つかまれているガイスターの、元のオブジェクト
    /// </summary>
    private GameObject _originalGeister = default;

    /// <summary>
    /// 配列オブジェクト
    /// </summary>
    private GameObject _boardObj = default;

    /// <summary>
    /// ボード管理スクリプト
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// タッチしてるポジションX
    /// </summary>
    private int _touchPositionX;
    /// <summary>
    /// タッチポジションY
    /// </summary>
    private int _touchPositionZ;

    /// <summary>
    /// ガイスター配置用オブジェクト
    /// </summary>
    [SerializeField]private GameObject[] _geisterSetPixels = new GameObject[8];

    /// <summary>
    /// 自分のターンかどうか
    /// </summary>
    public bool _myTurn { get; set; } = false;

    /// <summary>
    /// ガイスターセット中かどうか
    /// </summary>
    public bool _setGeister { get; set; } = false;
    /// <summary>
    /// セットした数
    /// </summary>
    private int _setCount = 0;

    /// <summary>
    /// 残りガイスター
    /// </summary>
    private int _residueGeister = 8;

    /// <summary>
    /// 青ガイスター残り
    /// </summary>
    private int _residueBlueGeister = 4;
    /// <summary>
    /// 赤ガイスター残り
    /// </summary>
    private int _residueRedGeister = 4;

    /// <summary>
    /// ガイスターの色青
    /// </summary>
    private const int Blue = 0;
    /// <summary>
    /// ガイスターの色赤
    /// </summary>
    private const int Red = 1;

    /// <summary>
    /// キャンバス
    /// </summary>
    private GameObject _canvas;
    /// <summary>
    /// ガイスター置いてくださいテキストのオブジェクト
    /// </summary>
    private GameObject _setTextObj;
    /// <summary>
    /// ガイスター置いてくださいテキストのオブジェクト
    /// </summary>
    private Text _setText;

    private void Awake()
    {
        _touchManager = new TouchManager();
        _boardObj = GameObject.FindGameObjectWithTag("Board");
        _geisterPrefab = Resources.Load<GameObject>("Prefab/DefaultGeister");
        _boardManager = _boardObj.GetComponent<BoardManager>();
        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _setTextObj = _canvas.transform.GetChild(0).gameObject;
        _setText = _setTextObj.GetComponent<Text>();
    }

    private void Update()
    {
        _touchManager.update();

        // タッチ取得
        TouchManager touch_state = this._touchManager.GetTouch();

        //タッチされているとき
        if (touch_state.TouchFlag)
        {
            _touchPositionX = (int)Math.Round(touch_state.TouchPosition.x, MidpointRounding.AwayFromZero);
            _touchPositionZ = (int)Math.Round(touch_state.TouchPosition.z - 0.2f, MidpointRounding.AwayFromZero);
            // タッチした瞬間の処理
            if (touch_state.TouchPhase == TouchPhase.Began)
            {
                //自分のターンの時
                if (_myTurn)
                {
                    CreateGeister(touch_state);
                    _boardManager.SetPixelCanChoice(_originalGeister.transform);
                }
                //ガイスターセットの時
                if (_setGeister)
                {
                    if (_boardManager.ToIdx(_touchPositionX, _touchPositionZ) > 0 && _boardManager.ToIdx(_touchPositionX, _touchPositionZ) < 5 ||
                           _boardManager.ToIdx(_touchPositionX, _touchPositionZ) > 6 && _boardManager.ToIdx(_touchPositionX, _touchPositionZ) < 11)
                    {
                    }
                    else return;
                    if (_setCount < 4)
                    {
                        
                        if (_boardManager.GetPixel(_touchPositionX, _touchPositionZ).GetComponent<Pixel>().SetGeisterBlue("Player")) _setCount++;
                        if (_setCount == 4)
                        {
                            _setText.text = "赤のガイスターを配置してください";
                            _setText.color = new Color(255, 0, 0);
                        }
                        
                    }
                    else if (_setCount < 8)
                    {
                        if (_boardManager.GetPixel(_touchPositionX, _touchPositionZ).GetComponent<Pixel>().SetGeisterRed("Player")) _setCount++;
                        if (_setCount == 8) 
                        {
                            _setText.text = "";
                            _setGeister = false;
                            _myTurn = true;
                            //_boardManager.SetPixelChoice();
                        } 
                    }
                    
                }
            }

            if (_chachedGeister != null)
            {
                _chachedGeister.transform.position = new Vector3(touch_state.TouchPosition.x,_chachedGeister.transform.position.y,touch_state.TouchPosition.z) ;

                int deltaPosX = (int)Mathf.Round(_chachedGeister.transform.position.x - _originalGeister.transform.position.x);
                int deltaPosZ = (int)Mathf.Round(_chachedGeister.transform.position.z - _originalGeister.transform.position.z);

                //離したら
                if (touch_state.TouchPhase == TouchPhase.Ended)
                {

                    EndedTouch(deltaPosX, deltaPosZ);
                    
                }
            }
        }
    }

    /// <summary>
    /// ガイスター移動先選択の時にガイスター表示するやつ
    /// </summary>
    private void CreateGeister(TouchManager touch_state)
    {

        //配列の外なら
        if (_boardManager.GetValue(_touchPositionX, _touchPositionZ,_boardManager.ArrayBoard) == -1 || _boardManager.GetValue(_touchPositionX, _touchPositionZ,_boardManager.ArrayBoard) == 99)
        {
            Debug.Log("配列の範囲外です");
            return;
        }
        //何もないなら
        if(_boardManager.GetValue(_touchPositionX, _touchPositionZ, _boardManager.ArrayBoard) == 0)
        {
            Debug.Log("空白です");
            return;
        }
        _originalGeister = _boardManager.GetObject(_touchPositionX,_touchPositionZ);
        _chachedGeister = Instantiate(_boardManager.GetObject((int)Mathf.Round(touch_state.TouchPosition.x), (int)Mathf.Round(touch_state.TouchPosition.z)), 
                                      new Vector3(touch_state.TouchPosition.x, _geisterPrefab.transform.position.y, touch_state.TouchPosition.z), 
                                      _boardManager.GetObject((int)Mathf.Round(touch_state.TouchPosition.x), (int)Mathf.Round(touch_state.TouchPosition.z)).transform.rotation);
    }

    /// <summary>
    /// タッチしている奴離したとき
    /// </summary>
    private void EndedTouch(int deltaPosX,int deltaPosZ)
    {
        if (Mathf.Abs(deltaPosX) == 1 || Math.Abs(deltaPosZ) == 1)
        {
            //置こうとした場所におけるかどうか確認
            if (_boardManager.CheckCanMoveThis(deltaPosX, deltaPosZ, _originalGeister.transform))
            {
                _originalGeister.transform.position = new Vector3(_originalGeister.transform.position.x + deltaPosX, _originalGeister.transform.position.y, _originalGeister.transform.position.z + deltaPosZ);
                _myTurn = false;
            }
        }
        _boardManager.ResetPixelCanChoice();
        Destroy(_chachedGeister);
        _originalGeister = null;

    }

    /// <summary>
    /// ガイスター取られたとき
    /// </summary>
    public void ResidueCountDown(GameObject geister)
    {
        if (geister.GetComponent<Geister>()._color == Blue)
        {
            _residueBlueGeister--;

        }
        else if (geister.GetComponent<Geister>()._color == Red)
        {
            _residueRedGeister--;
        }

        _residueGeister--;

        //青無くなった
        if (_residueBlueGeister == 0)
        {
            //敵勝利
        }
        //赤無くなった
        if (_residueRedGeister == 0)
        {
            //プレイヤー勝利
        }

    }
}
