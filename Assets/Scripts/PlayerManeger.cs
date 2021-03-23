using System.Collections;
using System;
using UnityEngine;
using StateManager;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�Ǘ��X�N���v�g
/// </summary>
public class PlayerManeger : MonoBehaviour
{
    /// <summary>
    /// �^�b�`�Ǘ��X�N���v�g
    /// </summary>
    private TouchManager _touchManager=default;

    /// <summary>
    /// �v���C���[�̃K�C�X�^�[�I�u�W�F�N�g�z��
    /// </summary>
    private GameObject[] _playerGeister=new GameObject[8];

    /// <summary>
    /// �K�C�X�^�[����ł�Ƃ��ɕ\��������
    /// </summary>
    private GameObject _geisterPrefab = default;

    /// <summary>
    /// ���܂�Ă�K�C�X�^�[
    /// </summary>
    private GameObject _chachedGeister=default;

    /// <summary>
    /// ���܂�Ă���K�C�X�^�[�́A���̃I�u�W�F�N�g
    /// </summary>
    private GameObject _originalGeister = default;

    /// <summary>
    /// �z��I�u�W�F�N�g
    /// </summary>
    private GameObject _boardObj = default;

    /// <summary>
    /// �{�[�h�Ǘ��X�N���v�g
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// �^�b�`���Ă�|�W�V����X
    /// </summary>
    private int _touchPositionX;
    /// <summary>
    /// �^�b�`�|�W�V����Y
    /// </summary>
    private int _touchPositionZ;

    /// <summary>
    /// �K�C�X�^�[�z�u�p�I�u�W�F�N�g
    /// </summary>
    [SerializeField]private GameObject[] _geisterSetPixels = new GameObject[8];

    /// <summary>
    /// �����̃^�[�����ǂ���
    /// </summary>
    public bool _myTurn { get; set; } = false;

    /// <summary>
    /// �K�C�X�^�[�Z�b�g�����ǂ���
    /// </summary>
    public bool _setGeister { get; set; } = false;
    /// <summary>
    /// �Z�b�g������
    /// </summary>
    private int _setCount = 0;

    /// <summary>
    /// �c��K�C�X�^�[
    /// </summary>
    private int _residueGeister = 8;

    /// <summary>
    /// �K�C�X�^�[�c��
    /// </summary>
    private int _residueBlueGeister = 4;
    /// <summary>
    /// �ԃK�C�X�^�[�c��
    /// </summary>
    private int _residueRedGeister = 4;

    /// <summary>
    /// �K�C�X�^�[�̐F��
    /// </summary>
    private const int Blue = 0;
    /// <summary>
    /// �K�C�X�^�[�̐F��
    /// </summary>
    private const int Red = 1;

    /// <summary>
    /// �L�����o�X
    /// </summary>
    private GameObject _canvas;
    /// <summary>
    /// �K�C�X�^�[�u���Ă��������e�L�X�g�̃I�u�W�F�N�g
    /// </summary>
    private GameObject _setTextObj;
    /// <summary>
    /// �K�C�X�^�[�u���Ă��������e�L�X�g�̃I�u�W�F�N�g
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

        // �^�b�`�擾
        TouchManager touch_state = this._touchManager.GetTouch();

        //�^�b�`����Ă���Ƃ�
        if (touch_state.TouchFlag)
        {
            _touchPositionX = (int)Math.Round(touch_state.TouchPosition.x, MidpointRounding.AwayFromZero);
            _touchPositionZ = (int)Math.Round(touch_state.TouchPosition.z - 0.2f, MidpointRounding.AwayFromZero);
            // �^�b�`�����u�Ԃ̏���
            if (touch_state.TouchPhase == TouchPhase.Began)
            {
                //�����̃^�[���̎�
                if (_myTurn)
                {
                    CreateGeister(touch_state);
                    _boardManager.SetPixelCanChoice(_originalGeister.transform);
                }
                //�K�C�X�^�[�Z�b�g�̎�
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
                            _setText.text = "�Ԃ̃K�C�X�^�[��z�u���Ă�������";
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

                //��������
                if (touch_state.TouchPhase == TouchPhase.Ended)
                {

                    EndedTouch(deltaPosX, deltaPosZ);
                    
                }
            }
        }
    }

    /// <summary>
    /// �K�C�X�^�[�ړ���I���̎��ɃK�C�X�^�[�\��������
    /// </summary>
    private void CreateGeister(TouchManager touch_state)
    {

        //�z��̊O�Ȃ�
        if (_boardManager.GetValue(_touchPositionX, _touchPositionZ,_boardManager.ArrayBoard) == -1 || _boardManager.GetValue(_touchPositionX, _touchPositionZ,_boardManager.ArrayBoard) == 99)
        {
            Debug.Log("�z��͈̔͊O�ł�");
            return;
        }
        //�����Ȃ��Ȃ�
        if(_boardManager.GetValue(_touchPositionX, _touchPositionZ, _boardManager.ArrayBoard) == 0)
        {
            Debug.Log("�󔒂ł�");
            return;
        }
        _originalGeister = _boardManager.GetObject(_touchPositionX,_touchPositionZ);
        _chachedGeister = Instantiate(_boardManager.GetObject((int)Mathf.Round(touch_state.TouchPosition.x), (int)Mathf.Round(touch_state.TouchPosition.z)), 
                                      new Vector3(touch_state.TouchPosition.x, _geisterPrefab.transform.position.y, touch_state.TouchPosition.z), 
                                      _boardManager.GetObject((int)Mathf.Round(touch_state.TouchPosition.x), (int)Mathf.Round(touch_state.TouchPosition.z)).transform.rotation);
    }

    /// <summary>
    /// �^�b�`���Ă���z�������Ƃ�
    /// </summary>
    private void EndedTouch(int deltaPosX,int deltaPosZ)
    {
        if (Mathf.Abs(deltaPosX) == 1 || Math.Abs(deltaPosZ) == 1)
        {
            //�u�����Ƃ����ꏊ�ɂ����邩�ǂ����m�F
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
    /// �K�C�X�^�[���ꂽ�Ƃ�
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

        //�����Ȃ���
        if (_residueBlueGeister == 0)
        {
            //�G����
        }
        //�Ԗ����Ȃ���
        if (_residueRedGeister == 0)
        {
            //�v���C���[����
        }

    }
}
