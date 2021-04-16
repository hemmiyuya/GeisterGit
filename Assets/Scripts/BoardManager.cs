using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �z��Ǘ��X�N���v�g
/// </summary>
public class BoardManager : MonoBehaviour
{
    /// <summary>
    /// �͈͊O�w�莞�̕Ԃ�l
    /// </summary>
    private const int _outOfRengeReturn = 99;
    /// <summary>
    /// �}�X�ڑ���
    /// </summary>
    private int _totalPixel = 0;
    /// <summary>
    /// �{�[�h�̔z��
    /// </summary>
    [SerializeField] public int[] ArrayBoard { get; private set; } = null;
    private int[] _goalIndex = { 0, 7,40,47 };
    /// <summary>
    /// �{�[�h�z��̃I�u�W�F�N�g
    /// </summary>
    [SerializeField] private GameObject[] _arrayBoardObject = null;
    /// <summary>
    /// �K�C�X�^�[�����p
    /// </summary>
    private GameObject _geister = default;
    /// <summary>
    /// �}�X�ڃI�u�W�F�N�g
    /// </summary>
    [SerializeField] private GameObject[] _arrayPixel = null;
    /// <summary>
    /// �A�E�g���C���\�����}�X��
    /// </summary>
    private GameObject[] _outLinePixel = new GameObject[4];
    /// <summary>
    /// �}�X�ڐ����p
    /// </summary>
    private GameObject _pixel = default;

    /// <summary>
    /// ���擾
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// �����擾
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// �v���C���[�}�l�[�W���[�X�N���v�g
    /// </summary>
    private PlayerManeger _playerManager = default;
    /// <summary>
    /// �G�Ǘ��X�N���v�g
    /// </summary>
    private EnemyManager _enemyManager = default;
    /// <summary>
    /// AI�Ǘ��X�N���v�g
    /// </summary>
    private AIManager _aiManager = default;

    private void Awake()
    {
        _geister = Resources.Load<GameObject>("Prefab/DefaultGeister");
        _pixel = Resources.Load<GameObject>("Prefab/DefaultPixel");
        _playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManeger>();
        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        _aiManager = GameObject.FindGameObjectWithTag("AIManager").GetComponent<AIManager>();
    }

    /// <summary>
    /// �z��쐬
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void CreateArray(int width, int height)
    {
        Width = width;
        Height = height;
        _totalPixel = Width * Height;
        ArrayBoard = new int[Width * Height];
        _arrayBoardObject = new GameObject[Width * Height];
        _arrayPixel = new GameObject[Width * Height];
        //������
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                if ((i != 0 || i != Height)&&(j==0||j==Width))
                {
                    ArrayBoard[i * Width + j] = 99;
                }
                else
                {
                    ArrayBoard[i * Width + j] = 0;
                }
            }
        }
        Dump(ArrayBoard);
    }

    /// <summary>
    /// �}�X�ڔz�u
    /// </summary>
    public void SetPixel()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                
                //�㉺�̗�ȊO�̒[��͐������Ȃ�
                if ((i == 0 || i == Height-1 || ( j != 0 && j != Width-1)))
                {
                    Debug.Log("i=" + i + "j=" + j);
                    Debug.Log("index="+(j+i*Width));
                    GameObject pixel = Instantiate(_pixel, ToPosition(j + i* Width), default);

                    _arrayPixel[j + i * Height] = pixel;
                    if ((j + i * Width > 1 && j + i * Width < 6) || (j + i * Width > 9 &&  j + i * Width < 14))
                    {
                        pixel.GetComponent<Pixel>().SetGeisterBefor();
                    }
                }
            }
        }
        _playerManager._setGeister = true;
    }

    /// <summary>
    /// �̈�O���ǂ����`�F�b�N����
    /// true���Ɨ̈�O
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool OutOfRange(int x, int z)
    {
        if (x < 0 || x >= Width) { return true; }
        if (z < 0 || z >= Height) { return true; }
        return false;
    }

    /// <summary>
    /// ���W���C���f�b�N�X��
    /// </summary>
    /// <param name="x">X���W</param>
    /// <param name="y">Y���W</param>
    /// <returns></returns>
    public int ToIdx(int x, int y)
    {
        return x + (y * Width);
    }

    /// <summary>
    /// �z��ԍ������W��
    /// </summary>
    /// <param name="index">�z��ԍ�</param>
    /// <returns></returns>
    public Vector3 ToPosition(int index)
    {
        float width;
        float height;
        height = index / Width;
        width = index % Width;
        return new Vector3(width, 0, height);
    }

    /// <summary>
    /// �w�肵�����W�̒l���擾
    /// </summary>
    /// <param name="x">x���W</param>
    /// <param name="z">y���W</param>
    /// <returns></returns>
    public int GetValue(int x, int z,int[] array)
    {
        if (OutOfRange(x, z))
        {
            return _outOfRengeReturn;
        }

        return array[z * Width + x];
    }

    /// <summary>
    /// �w�肵�����W�̃I�u�W�F�N�g�擾
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public GameObject GetObject(int x, int z)
    {
        if (OutOfRange(x, z))
        {
            return null;
        }

        return _arrayBoardObject[z * Width + x];
    }

    /// <summary>
    /// �w�肵�����W�̃}�X�擾
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public GameObject GetPixel(int x, int z)
    {
        if (OutOfRange(x, z))
        {
            return null;
        }

        return _arrayPixel[z * Width + x];
    }

    /// <summary>
    /// �w�肵�����W�ɒl������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="value"></param>
    public void SetValue(int x, int z, int value,int[] l_Array)
    {
        if (OutOfRange(x, z))
        {
            return;
        }

        l_Array[z * Width + x] = value;
    }

    /// <summary>
    /// �w�肵�����W�ɃI�u�W�F�N�g������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="gameObject"></param>
    public void SetObject(int x, int z, GameObject gameObject)
    {
        if (OutOfRange(x, z))
        {
            return;
        }
        _arrayBoardObject[z * Width + x] = gameObject;
    }

    /// <summary>
    /// �o�͗p���\�b�h
    /// </summary>
    public void Dump(int[] array)
    {
        Debug.Log("[Board] (w,h)=(" + Width + "," + Height + ")");
        for (int z = 0; z < Height; z++)
        {
            string s = "";
            for (int x = 0; x < Width; x++)
            {
                s += GetValue(x, z, array) + ",";
            }
            Debug.Log(s);
        }
    }

    /// <summary>
    /// �w�肵�����W�Ɉړ��ł��邩�ǂ����m�F
    /// </summary>
    /// <param name="deltaX"></param>
    /// <param name="deltaZ"></param>
    /// <param name="originalGeristerTrs"></param>
    public bool CheckCanMoveThis(int deltaX, int deltaZ, Transform originalGeristerTrs)
    {
        //XZ�������l�������Ă����甲����
        if (Mathf.Abs(deltaX) == 1 && Mathf.Abs(deltaZ) == 1) return false;

        if (GetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ,ArrayBoard) == 0)
        {
            //�ړ�����
            SetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ, GetValue((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, ArrayBoard),ArrayBoard);
            SetObject((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ, originalGeristerTrs.gameObject);
            SetValue((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, 0,ArrayBoard);
            SetObject((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, null);

            return true;
        }
        else if (GetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ, ArrayBoard) == 1 ||
                 GetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ, ArrayBoard) == 2)
        {
            if (GetObject((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ).tag == "Enemy")
            {
                //�ړ�����

                SetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ, GetValue((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, ArrayBoard),ArrayBoard);
                SetValue((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, 0,ArrayBoard);
                return true;
            }
            else return false;
        }
        else return false;

    }

    /// <summary>
    /// �K�C�X�^�[�𓮂�����}�X�ڂɃA�E�g���C���\��
    /// </summary>
    public void SetPixelCanChoice(Transform geister)
    {
        int i = 0;
        //�E
        //�����Ȃ���
        if (GetValue((int)geister.position.x + 1, (int)geister.position.z, ArrayBoard) == 0)
        {
            _outLinePixel[i] = GetPixel((int)geister.position.x + 1, (int)geister.position.z);
            if (_outLinePixel[i] == null) return;
            Debug.Log(_outLinePixel[i].name);
            _outLinePixel[i].GetComponent<Pixel>().SetOutLine();

        }
        else if (GetObject((int)geister.position.x + 1, (int)geister.position.z) != null)
        {
            //�^�O�������̃K�C�X�^�[�ƈႤ��(�G�̎�)
            if (GetObject((int)geister.position.x + 1, (int)geister.position.z).tag!= geister.gameObject.tag)
            {
                _outLinePixel[i] = GetPixel((int)geister.position.x + 1, (int)geister.position.z);
                if (_outLinePixel[i] == null) return;
                Debug.Log(_outLinePixel[i].name);
                _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
            }
        }
        i++;
        //��
        if (GetValue((int)geister.position.x - 1, (int)geister.position.z, ArrayBoard) == 0)
        {
            _outLinePixel[i] = GetPixel((int)geister.position.x - 1, (int)geister.position.z);
            if (_outLinePixel[i] == null) return;
            _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
        }
        else if (GetObject((int)geister.position.x - 1, (int)geister.position.z) != null)
        {
            if (GetObject((int)geister.position.x - 1, (int)geister.position.z).tag != geister.gameObject.tag)
            {
                _outLinePixel[i] = GetPixel((int)geister.position.x - 1, (int)geister.position.z);
                if (_outLinePixel[i] == null) return;
                _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
            }
        }
        i++;
        //��
        if (GetValue((int)geister.position.x, (int)geister.position.z + 1, ArrayBoard) == 0)
        {
            _outLinePixel[i] = GetPixel((int)geister.position.x, (int)geister.position.z + 1);
            if (_outLinePixel[i] == null) return;
            _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
        }
        else if(GetObject((int)geister.position.x, (int)geister.position.z + 1) != null)
        {
            if(GetObject((int)geister.position.x, (int)geister.position.z + 1).tag != geister.gameObject.tag)
            {
                _outLinePixel[i] = GetPixel((int)geister.position.x, (int)geister.position.z + 1);
                if (_outLinePixel[i] == null) return;
                _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
            }
        }
        i++;
        //��
        if (GetValue((int)geister.position.x, (int)geister.position.z - 1, ArrayBoard) == 0)
        {
            _outLinePixel[i] = GetPixel((int)geister.position.x, (int)geister.position.z - 1);
            if (_outLinePixel[i] == null) return;
            _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
        }
        else if(GetObject((int)geister.position.x, (int)geister.position.z - 1) != null)
        {
            if(GetObject((int)geister.position.x, (int)geister.position.z - 1).tag != geister.gameObject.tag)
            {
                _outLinePixel[i] = GetPixel((int)geister.position.x, (int)geister.position.z - 1);
                if (_outLinePixel[i] == null) return;
                _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
            }
        }
    }

    /// <summary>
    /// ���������A�E�g���C������
    /// </summary>
    public void ResetPixelCanChoice()
    {
        for (int i = 0; i < _outLinePixel.Length; i++)
        {
            if (_outLinePixel[i] == null) continue;
            _outLinePixel[i].GetComponent<Pixel>().ResetOutLine();
        }
    }

    /// <summary>
    /// �ՖʑS�̂��݂āA��������I�u�W�F�N�g��T��
    /// </summary>
    public List<int> CheckCanMoveAll(int[] array,int turn)
    {
        List<int> canMoveIndexs = new List<int>();
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                if (CheckMove(x, z,array,turn))
                {
                    canMoveIndexs.Add(ToIdx(x, z));
                }
            }
        }
        return canMoveIndexs;
    }

    /// <summary>
    /// �������邩�ǂ���
    /// </summary>
    /// <returns></returns>
    private bool CheckMove(int x,int z,int[] array,int turn)
    {
        if (turn == Turn.PLAYERTURN)
        {
            if (GetValue(x, z, array) == 11 || GetValue(x, z, array) == 12)
            {

                //�E
                if (GetValue((int)x + 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x + 1, (int)z, array) == 01 || GetValue((int)x + 1, (int)z, array) == 02)
                {
                    return true;
                }

                //��
                if (GetValue((int)x - 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x - 1, (int)z, array) == 01 || GetValue((int)x - 1, (int)z, array) == 02)
                {
                    return true;
                }

                //��
                if (GetValue((int)x, (int)z + 1, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x, (int)z + 1, array) == 01 || GetValue((int)x + 1, (int)z + 1, array) == 02)
                {
                    return true;
                }

                //��
                if (GetValue((int)x, (int)z - 1, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x, (int)z + 1, array) == 01 || GetValue((int)x + 1, (int)z + 1, array) == 02)
                {
                    return true;
                }

                return false;


            }
            else return false;
        }
        else if (turn == Turn.ENEMYTURN)
        {
            if (GetValue(x, z, array) == 11 || GetValue(x, z, array) == 12)
            {

                //�E
                if (GetValue((int)x + 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x + 1, (int)z, array) == 01 || GetValue((int)x + 1, (int)z, array) == 02)
                {
                    return true;
                }

                //��
                if (GetValue((int)x - 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x - 1, (int)z, array) == 01 || GetValue((int)x - 1, (int)z, array) == 02)
                {
                    return true;
                }

                //��
                if (GetValue((int)x, (int)z + 1, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x, (int)z + 1, array) == 01 || GetValue((int)x + 1, (int)z + 1, array) == 02)
                {
                    return true;
                }

                //��
                if (GetValue((int)x, (int)z - 1, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x, (int)z + 1, array) == 01 || GetValue((int)x + 1, (int)z + 1, array) == 02)
                {
                    return true;
                }

                return false;


            }
            else return false;
        }
        else
        {
            Debug.Log("�������^�[����^���Ă�������");
            return false;
        }
    }

    /// <summary>
    /// �w�肵�������ɓ������邩�ǂ����m�F����B��������Ȃ瓮�����āA����������̔z���Ԃ�
    /// </summary>
    /// <param name="l_array">�z��</param>
    /// <param name="l_Index">�C���f�b�N�X</param>
    public int[] DesignationDirectionCanMove(int[] l_Array,int l_Index,DirectionEnum l_DirectionEnum,int l_Turn)
    {
        int x = (int)ToPosition(l_Index).x;
        int z = (int)ToPosition(l_Index).z;
        //�����^�[��
        if (l_Turn == Turn.PLAYERTURN)
        {
            //��
            if (l_DirectionEnum == DirectionEnum.Up)
            {
                if (GetValue(x, z + 1, l_Array) == (int)BoardValue.Null || GetValue(x, z + 1, l_Array) == (int)BoardValue.EnemyBlue || GetValue(x, z + 1, l_Array) == (int)BoardValue.EnemyRed)
                {
                    SetValue(x, z + 1, GetValue(x, z, l_Array),l_Array);
                    SetValue(x, z, 0,l_Array);
                }
            }
            else if(l_DirectionEnum == DirectionEnum.Right)
            {
                if (GetValue(x + 1, z , l_Array) == (int)BoardValue.Null || GetValue(x + 1, z, l_Array) == (int)BoardValue.EnemyBlue || GetValue(x + 1, z, l_Array) == (int)BoardValue.EnemyRed)
                {
                    SetValue(x+1, z, GetValue(x, z, l_Array),l_Array);
                    SetValue(x, z, 0,l_Array);
                }
            }
            else if (l_DirectionEnum == DirectionEnum.Down)
            {
                if (GetValue(x, z - 1, l_Array) == (int)BoardValue.Null || GetValue(x, z - 1, l_Array) == (int)BoardValue.EnemyBlue || GetValue(x, z - 1, l_Array) == (int)BoardValue.EnemyRed)
                {
                    SetValue(x , z-1, GetValue(x, z, l_Array), l_Array);
                    SetValue(x, z, 0, l_Array);
                }
            }
            else if (l_DirectionEnum == DirectionEnum.Right)
            {
                if (GetValue(x - 1, z, l_Array) == (int)BoardValue.Null || GetValue(x - 1, z, l_Array) == (int)BoardValue.EnemyBlue || GetValue(x - 1, z, l_Array) == (int)BoardValue.EnemyRed)
                {
                    SetValue(x - 1, z, GetValue(x, z, l_Array), l_Array);
                    SetValue(x, z, 0, l_Array);
                }
            }
        }
        //�G�^�[��
        else if (l_Turn == Turn.ENEMYTURN)
        {
            //��
            if (l_DirectionEnum == DirectionEnum.Up)
            {
                if (GetValue(x, z + 1, l_Array) == (int)BoardValue.Null || GetValue(x, z + 1, l_Array) == (int)BoardValue.PlayerBlue || GetValue(x, z + 1, l_Array) == (int)BoardValue.PlayerRed)
                {
                    SetValue(x, z + 1, GetValue(x, z, l_Array), l_Array);
                    SetValue(x, z, 0, l_Array);
                }
            }
            else if (l_DirectionEnum == DirectionEnum.Right)
            {
                if (GetValue(x + 1, z, l_Array) == (int)BoardValue.Null || GetValue(x + 1, z, l_Array) == (int)BoardValue.PlayerBlue || GetValue(x + 1, z, l_Array) == (int)BoardValue.PlayerRed)
                {
                    SetValue(x + 1, z, GetValue(x, z, l_Array), l_Array);
                    SetValue(x, z, 0, l_Array);
                }
            }
            else if (l_DirectionEnum == DirectionEnum.Down)
            {
                if (GetValue(x, z - 1, l_Array) == (int)BoardValue.Null || GetValue(x, z - 1, l_Array) == (int)BoardValue.PlayerBlue || GetValue(x, z - 1, l_Array) == (int)BoardValue.PlayerRed)
                {
                    SetValue(x, z - 1, GetValue(x, z, l_Array), l_Array);
                    SetValue(x, z, 0, l_Array);
                }
            }
            else if (l_DirectionEnum == DirectionEnum.Right)
            {
                if (GetValue(x - 1, z, l_Array) == (int)BoardValue.Null || GetValue(x - 1, z, l_Array) == (int)BoardValue.PlayerBlue || GetValue(x - 1, z, l_Array) == (int)BoardValue.PlayerRed)
                {
                    SetValue(x - 1, z, GetValue(x, z, l_Array), l_Array);
                    SetValue(x, z, 0, l_Array);
                }
            }
        }
        return l_Array;
    }

    /// <summary>
    /// �E���̃S�[���Ɉ�ԋ߂��K�C�X�^�[��T���āA�C���f�b�N�X��Ԃ�
    /// </summary>
    public int CheckGeisterRight()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 2; x < Width; x++)
            {
                if(GetValue(x, y,ArrayBoard) != 0)
                {
                    if (GetObject(x, y).tag == "Enemy")
                    {
                        return ToIdx(x, y);
                    }
                }
            }
        }
        return default;
    }

    /// <summary>
    /// �����̃S�[���Ɉ�ԋ߂��K�C�X�^�[��T���āA�C���f�b�N�X��Ԃ�
    /// </summary>
    public int CheckGeisterLeft()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width*0.5f; x++)
            {
                if (GetValue(x, y,ArrayBoard) != 0)
                {
                    if (GetObject(x, y).tag == "Enemy")
                    {
                        return ToIdx(x, y);
                    }
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// �v���C���[�̋�̃C���f�b�N�X�ꗗ�����X�g�ɂ܂Ƃ߂ĕԂ�
    /// </summary>
    /// <param name="array">����z��</param>
    /// <returns>�v���C���[�̋�̃C���f�b�N�X�ꗗ</returns>
    public List<int> FindPlayerGeister(int[] array)
    {
        List<int> list = new List<int>();
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                if(GetValue(x,z,array)==(int)BoardValue.PlayerBlue|| GetValue(x, z, array) == (int)BoardValue.PlayerRed)
                {
                    list.Add(ToIdx(x, z));
                }
            }
        }
        return list;
    }
    
    /// <summary>
    /// �F�w�肷��ƌ��߂�ꂽ�F�����擾
    /// </summary>
    /// <param name="array"></param>
    /// <param name="l_Color"></param>
    /// <returns></returns>
    public List<int> FindPlayerGeister(int[] array,int l_Color)
    {
        List<int> list = new List<int>();
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                if (l_Color == (int)BoardValue.PlayerBlue)
                {
                    if (GetValue(x, z, array) == (int)BoardValue.PlayerBlue)
                    {
                        list.Add(ToIdx(x, z));
                    }
                }
                if (l_Color == (int)BoardValue.PlayerRed)
                {
                    if (GetValue(x, z, array) == (int)BoardValue.PlayerRed)
                    {
                        list.Add(ToIdx(x, z));
                    }
                }

            }
        }
        return list;
    }

    /// <summary>
    /// �G�̋�̃C���f�b�N�X�ꗗ�����X�g�ɂ܂Ƃ߂ĕԂ�
    /// </summary>
    /// <param name="array">����z��</param>
    /// <returns>�G�̋�̃C���f�b�N�X�ꗗ</returns>
    public List<int> FindEnemyGeister(int[] array)
    {
        List<int> list = new List<int>();
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                if (GetValue(x, z, array) == (int)BoardValue.EnemyBlue || GetValue(x, z, array) == (int)BoardValue.EnemyRed)
                {
                    list.Add(ToIdx(x, z));
                }
            }
        }
        return list;
    }

    public List<int> FindEnemyGeister(int[] array, int l_Color)
    {
        List<int> list = new List<int>();
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                if (l_Color == (int)BoardValue.EnemyBlue)
                {
                    if (GetValue(x, z, array) == (int)BoardValue.EnemyBlue)
                    {
                        list.Add(ToIdx(x, z));
                    }
                }
                if (l_Color == (int)BoardValue.EnemyBlue)
                {
                    if (GetValue(x, z, array) == (int)BoardValue.EnemyBlue)
                    {
                        list.Add(ToIdx(x, z));
                    }
                }
            }
        }
        return list;
    }

    /// <summary>
    /// ���s�����܂��Ă��邩�ǂ����m�F
    /// �z��1�����s���܂��Ă��邩�ǂ���
    /// �z��2���ǂ��炪��������(true=Player,false=Enemy)
    /// </summary>
    /// <returns></returns>
    public bool[] ClearCheck(int[] l_ArrayBoard)
    {
        bool[] l_ClearFrag=new bool[2];
        l_ClearFrag[0] = false;
        l_ClearFrag[1] = false;
        if (FindPlayerGeister(l_ArrayBoard, (int)BoardValue.PlayerRed).Count == 0 ||
           FindPlayerGeister(l_ArrayBoard, (int)BoardValue.EnemyBlue).Count == 0)
        {
            //�v���C���[�̐Ԃ������Ȃ�A�������͓G�̐��Ȃ��Ȃ�
            l_ClearFrag[0] = true;
            l_ClearFrag[1] = true;
        }
        else
        if(FindPlayerGeister(l_ArrayBoard, (int)BoardValue.PlayerBlue).Count==0||
           FindPlayerGeister(l_ArrayBoard, (int)BoardValue.EnemyRed).Count == 0)
        {
            //�G�̐Ԃ������Ȃ�A�������̓v���C���[�̐��Ȃ��Ȃ�
            l_ClearFrag[0] = true;
            l_ClearFrag[1] = false;
        }
        else
        {
            //�S�[�����Ă���
            for(var i = 0; i < 4; i++)
            {
                if (l_ArrayBoard[_goalIndex[i]] != 0)
                {
                    if (i<=1)
                    {
                        l_ClearFrag[0] = true;
                        l_ClearFrag[1] = false;
                    }
                    else
                    {
                        l_ClearFrag[0] = true;
                        l_ClearFrag[1] = true;
                    }
                }
            }
        }
        return l_ClearFrag;
    }

}
