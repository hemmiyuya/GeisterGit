using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ガイスター(駒)スクリプト
/// </summary>
public class Geister : MonoBehaviour
{
    /// <summary>
    /// プレイヤー管理スクリプト
    /// </summary>
    private PlayerManeger _playerManeger = default;

    /// <summary>
    /// 色 0=青,1=赤
    /// </summary>
    [SerializeField] public int _color { get; set; } = default;

    /// <summary>
    /// 背中の色のオブジェクト
    /// </summary>
    private GameObject _colorObj = default;

    /// <summary>
    /// 青マテリアル
    /// </summary>
    private Material _materialBlue = default;

    /// <summary>
    /// 赤マテリアル
    /// </summary>
    private Material _materialRed = default;

    /// <summary>
    /// 基本のレイヤー
    /// </summary>
    private const int _defaultLayer = 0;
    /// <summary>
    /// アウトライン表示用レイヤー
    /// </summary>
    private const int _outLineLayer = 6;

    /// <summary>
    /// 選択中かどうか
    /// </summary>
    public bool _choiceNow = default;


    private void Awake()
    {
        _playerManeger = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManeger>();
        _materialBlue = Resources.Load<Material>("Materials/BlueMat");
        _materialRed = Resources.Load<Material>("Materials/RedMat");

        _choiceNow = false;

        _colorObj = transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// マテリアルセット
    /// </summary>
    /// <param name="color"></param>
    public void SetMaterial(int color)
    {
        this._color = color;
        if (_color == 0)
        {
            _colorObj.GetComponent<Renderer>().material = _materialBlue;
        }
        else
        {
            _colorObj.GetComponent<Renderer>().material = _materialRed;
        }
    }

    /// <summary>
    /// マウスカーソル入ったら
    /// </summary>
    private void OnMouseEnter()
    {
        if (gameObject.tag == "Player"&&_playerManeger._myTurn)
        {
            gameObject.layer = _outLineLayer;
            _choiceNow = true;
        }
    }
    

    /// <summary>
    /// カーソル離れたら
    /// </summary>
    private void OnMouseExit()
    {
        if (gameObject.tag == "Player")
        {
            gameObject.layer = _defaultLayer;
            _choiceNow = false;
        }
    }
}
