using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �K�C�X�^�[(��)�X�N���v�g
/// </summary>
public class Geister : MonoBehaviour
{
    /// <summary>
    /// �v���C���[�Ǘ��X�N���v�g
    /// </summary>
    private PlayerManeger _playerManeger = default;

    /// <summary>
    /// �F 0=��,1=��
    /// </summary>
    [SerializeField] public int _color { get; set; } = default;

    /// <summary>
    /// �w���̐F�̃I�u�W�F�N�g
    /// </summary>
    private GameObject _colorObj = default;

    /// <summary>
    /// �}�e���A��
    /// </summary>
    private Material _materialBlue = default;

    /// <summary>
    /// �ԃ}�e���A��
    /// </summary>
    private Material _materialRed = default;

    /// <summary>
    /// ��{�̃��C���[
    /// </summary>
    private const int _defaultLayer = 0;
    /// <summary>
    /// �A�E�g���C���\���p���C���[
    /// </summary>
    private const int _outLineLayer = 6;

    /// <summary>
    /// �I�𒆂��ǂ���
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
    /// �}�e���A���Z�b�g
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
    /// �}�E�X�J�[�\����������
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
    /// �J�[�\�����ꂽ��
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
