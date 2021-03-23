using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�X�ڊǗ��X�N���v�g
/// </summary>
public class Pixel : MonoBehaviour
{
    /// <summary>
    /// ��{�̃��C���[
    /// </summary>
    public const int _defaultLayer = 0;
    /// <summary>
    /// �A�E�g���C���\���p���C���[
    /// </summary>
    public const int _outLineLayer = 6;

    /// <summary>
    /// �ŏ��̐F
    /// </summary>
    private Color _defaultColor;

    /// <summary>
    /// �K�C�X�^�[�Z�b�g����O�̐F
    /// </summary>
    [SerializeField] private Color _setBeforColor=new Color(0,255,0);

    /// <summary>
    /// �ړ��\�Ȏ��̐F
    /// </summary>
    [SerializeField]private Color _canMoveColor;

    /// <summary>
    /// �����̃����_���[
    /// </summary>
    private Renderer _renderer;

    /// <summary>
    /// �{�[�h�Ǘ��X�N���v�g
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// �ݒu����K�C�X�^�[
    /// </summary>
    private GameObject _setGeisterBlue;
    private GameObject _setGeisterRed;

    /// <summary>
    /// �K�C�X�^�[�̐F��
    /// </summary>
    private const int Blue = 0;

    /// <summary>
    /// �K�C�X�^�[�̐F��
    /// </summary>
    private const int Red = 1;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
        _setGeisterBlue = Resources.Load<GameObject>("Prefab/GeisterBlue");
        _setGeisterRed = Resources.Load<GameObject>("Prefab/GeisterRed");
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
    }

    /// <summary>
    /// �K�C�X�^�[�̈ړ����I�𒆂��ǂ���
    /// </summary>
    public bool ChoiceNow
    {
        get; set;
    }
    = false;

    private void OnMouseEnter()
    {
        if(ChoiceNow)
        gameObject.layer = _outLineLayer;
    }

    private void OnMouseExit()
    {
        gameObject.layer = _defaultLayer;
    }

    private void OnMouseDown()
    {
        if (ChoiceNow)
        {
            
        }
    }

    /// <summary>
    /// �K�C�X�^�[�z�u�O
    /// </summary>
    public void SetGeisterBefor()
    {
        _renderer = GetComponent<Renderer>();
        ChoiceNow = true;
        _renderer.material.color = _setBeforColor;
    }

    /// <summary>
    /// �K�C�X�^�[����
    /// </summary>
    public bool SetGeisterBlue(string geisterTag)
    {
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
        _setGeisterBlue = Resources.Load<GameObject>("Prefab/DefaultGeister");
        _setGeisterRed = Resources.Load<GameObject>("Prefab/DefaultGeister");
        if (_boardManager.GetObject((int)Mathf.Round(transform.position.x), (int)Mathf.Round( transform.position.z))==null)
        {
            GameObject geister=default;

            if (geisterTag == "Player")
            {
                geister = Instantiate(_setGeisterBlue, new Vector3(transform.position.x, _setGeisterBlue.transform.position.y, transform.position.z), Quaternion.Euler(_setGeisterBlue.transform.rotation.eulerAngles.x, _setGeisterBlue. transform.rotation.eulerAngles.y, _setGeisterBlue. transform.rotation.eulerAngles.z));
                _boardManager.SetValue((int)transform.position.x, (int)transform.position.z, 01,_boardManager.ArrayBoard);
            }
            else if (geisterTag == "Enemy")
            {
                geister = Instantiate(_setGeisterBlue, new Vector3(transform.position.x, _setGeisterBlue.transform.position.y, transform.position.z), Quaternion.Euler(_setGeisterBlue.transform.rotation.eulerAngles.x , _setGeisterBlue.transform.rotation.eulerAngles.y + 180, _setGeisterBlue.transform.rotation.eulerAngles.z));
                _boardManager.SetValue((int)transform.position.x, (int)transform.position.z, 11,_boardManager.ArrayBoard);
            }
            
            _boardManager.SetObject((int)transform.position.x, (int)transform.position.z, geister);
            geister.tag = geisterTag;
            geister.GetComponent<Geister>().SetMaterial(Blue);
            SetGeisterAfter();
            return true;
        }
        return false;
    }

    /// <summary>
    /// �ԃK�C�X�^�[����
    /// </summary>
    /// <returns></returns>
    public bool SetGeisterRed(string geisterTag)
    {
        _boardManager = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardManager>();
        _setGeisterBlue = Resources.Load<GameObject>("Prefab/DefaultGeister");
        _setGeisterRed = Resources.Load<GameObject>("Prefab/DefaultGeister");
        if (_boardManager.GetObject((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.z)) == null)
        {
            GameObject geister = default;

            if (geisterTag == "Player")
            {
                geister = Instantiate(_setGeisterRed, new Vector3(transform.position.x, _setGeisterRed.transform.position.y, transform.position.z), Quaternion.Euler(_setGeisterRed.transform.rotation.eulerAngles.x, _setGeisterRed.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
                _boardManager.SetValue((int)transform.position.x, (int)transform.position.z, 02,_boardManager.ArrayBoard);
            }
            else if (geisterTag == "Enemy")
            {
                geister = Instantiate(_setGeisterRed, new Vector3(transform.position.x, _setGeisterRed.transform.position.y, transform.position.z), Quaternion.Euler(_setGeisterRed.transform.rotation.eulerAngles.x , _setGeisterRed. transform.rotation.eulerAngles.y + 180, _setGeisterRed. transform.rotation.eulerAngles.z));
                _boardManager.SetValue((int)transform.position.x, (int)transform.position.z, 12,_boardManager.ArrayBoard);
            }

            _boardManager.SetObject((int)transform.position.x, (int)transform.position.z, geister);
            
            geister.tag = geisterTag;
            geister.GetComponent<Geister>().SetMaterial(Red);
            SetGeisterAfter();
            return true;
        }
        return false;
    }

    /// <summary>
    /// �K�C�X�^�[�z�u��
    /// </summary>
    private void SetGeisterAfter()
    {
        ChoiceNow = false;
        gameObject.layer = _defaultLayer;
    }

    /// <summary>
    /// �A�E�g���C���\��
    /// </summary>
    public void SetOutLine()
    {
        gameObject.layer = _outLineLayer;
    }

    /// <summary>
    /// �A�E�g���C������
    /// </summary>
    public void ResetOutLine()
    {
        gameObject.layer = _defaultLayer;
    }

}
