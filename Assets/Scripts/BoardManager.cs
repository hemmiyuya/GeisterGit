using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 配列管理スクリプト
/// </summary>
public class BoardManager : MonoBehaviour
{
    /// <summary>
    /// 範囲外指定時の返り値
    /// </summary>
    private const int _outOfRengeReturn = 99;
    /// <summary>
    /// マス目総数
    /// </summary>
    private int _totalPixel = 0;
    /// <summary>
    /// ボードの配列
    /// </summary>
    [SerializeField] public int[] ArrayBoard { get; private set; } = null;
    private int[] _goalIndex = { 0, 7,40,47 };
    /// <summary>
    /// ボード配列のオブジェクト
    /// </summary>
    [SerializeField] private GameObject[] _arrayBoardObject = null;
    /// <summary>
    /// ガイスター生成用
    /// </summary>
    private GameObject _geister = default;
    /// <summary>
    /// マス目オブジェクト
    /// </summary>
    [SerializeField] private GameObject[] _arrayPixel = null;
    /// <summary>
    /// アウトライン表示中マス目
    /// </summary>
    private GameObject[] _outLinePixel = new GameObject[4];
    /// <summary>
    /// マス目生成用
    /// </summary>
    private GameObject _pixel = default;

    /// <summary>
    /// 幅取得
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// 高さ取得
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// プレイヤーマネージャースクリプト
    /// </summary>
    private PlayerManeger _playerManager = default;
    /// <summary>
    /// 敵管理スクリプト
    /// </summary>
    private EnemyManager _enemyManager = default;
    /// <summary>
    /// AI管理スクリプト
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
    /// 配列作成
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
        //初期化
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
    /// マス目配置
    /// </summary>
    public void SetPixel()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                
                //上下の列以外の端列は生成しない
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
    /// 領域外かどうかチェックする
    /// trueだと領域外
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
    /// 座標をインデックスに
    /// </summary>
    /// <param name="x">X座標</param>
    /// <param name="y">Y座標</param>
    /// <returns></returns>
    public int ToIdx(int x, int y)
    {
        return x + (y * Width);
    }

    /// <summary>
    /// 配列番号を座標に
    /// </summary>
    /// <param name="index">配列番号</param>
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
    /// 指定した座標の値を取得
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="z">y座標</param>
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
    /// 指定した座標のオブジェクト取得
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
    /// 指定した座標のマス取得
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
    /// 指定した座標に値を入れる
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
    /// 指定した座標にオブジェクトを入れる
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
    /// 出力用メソッド
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
    /// 指定した座標に移動できるかどうか確認
    /// </summary>
    /// <param name="deltaX"></param>
    /// <param name="deltaZ"></param>
    /// <param name="originalGeristerTrs"></param>
    public bool CheckCanMoveThis(int deltaX, int deltaZ, Transform originalGeristerTrs)
    {
        //XZ両方数値が入っていたら抜ける
        if (Mathf.Abs(deltaX) == 1 && Mathf.Abs(deltaZ) == 1) return false;

        if (GetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ,ArrayBoard) == 0)
        {
            //移動する
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
                //移動する

                SetValue((int)originalGeristerTrs.position.x + deltaX, (int)originalGeristerTrs.position.z + deltaZ, GetValue((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, ArrayBoard),ArrayBoard);
                SetValue((int)originalGeristerTrs.position.x, (int)originalGeristerTrs.position.z, 0,ArrayBoard);
                return true;
            }
            else return false;
        }
        else return false;

    }

    /// <summary>
    /// ガイスターを動かせるマス目にアウトライン表示
    /// </summary>
    public void SetPixelCanChoice(Transform geister)
    {
        int i = 0;
        //右
        //何もない時
        if (GetValue((int)geister.position.x + 1, (int)geister.position.z, ArrayBoard) == 0)
        {
            _outLinePixel[i] = GetPixel((int)geister.position.x + 1, (int)geister.position.z);
            if (_outLinePixel[i] == null) return;
            Debug.Log(_outLinePixel[i].name);
            _outLinePixel[i].GetComponent<Pixel>().SetOutLine();

        }
        else if (GetObject((int)geister.position.x + 1, (int)geister.position.z) != null)
        {
            //タグが自分のガイスターと違う時(敵の時)
            if (GetObject((int)geister.position.x + 1, (int)geister.position.z).tag!= geister.gameObject.tag)
            {
                _outLinePixel[i] = GetPixel((int)geister.position.x + 1, (int)geister.position.z);
                if (_outLinePixel[i] == null) return;
                Debug.Log(_outLinePixel[i].name);
                _outLinePixel[i].GetComponent<Pixel>().SetOutLine();
            }
        }
        i++;
        //左
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
        //上
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
        //下
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
    /// 動かせるよアウトライン解除
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
    /// 盤面全体をみて、動かせるオブジェクトを探す
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
    /// 動かせるかどうか
    /// </summary>
    /// <returns></returns>
    private bool CheckMove(int x,int z,int[] array,int turn)
    {
        if (turn == Turn.PLAYERTURN)
        {
            if (GetValue(x, z, array) == 11 || GetValue(x, z, array) == 12)
            {

                //右
                if (GetValue((int)x + 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x + 1, (int)z, array) == 01 || GetValue((int)x + 1, (int)z, array) == 02)
                {
                    return true;
                }

                //左
                if (GetValue((int)x - 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x - 1, (int)z, array) == 01 || GetValue((int)x - 1, (int)z, array) == 02)
                {
                    return true;
                }

                //上
                if (GetValue((int)x, (int)z + 1, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x, (int)z + 1, array) == 01 || GetValue((int)x + 1, (int)z + 1, array) == 02)
                {
                    return true;
                }

                //下
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

                //右
                if (GetValue((int)x + 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x + 1, (int)z, array) == 01 || GetValue((int)x + 1, (int)z, array) == 02)
                {
                    return true;
                }

                //左
                if (GetValue((int)x - 1, (int)z, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x - 1, (int)z, array) == 01 || GetValue((int)x - 1, (int)z, array) == 02)
                {
                    return true;
                }

                //上
                if (GetValue((int)x, (int)z + 1, array) == 0)
                {
                    return true;
                }
                else if (GetValue((int)x, (int)z + 1, array) == 01 || GetValue((int)x + 1, (int)z + 1, array) == 02)
                {
                    return true;
                }

                //下
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
            Debug.Log("正しいターンを与えてください");
            return false;
        }
    }

    /// <summary>
    /// 指定した方向に動かせるかどうか確認する。動かせるなら動かして、動かした後の配列を返す
    /// </summary>
    /// <param name="l_array">配列</param>
    /// <param name="l_Index">インデックス</param>
    public int[] DesignationDirectionCanMove(int[] l_Array,int l_Index,DirectionEnum l_DirectionEnum,int l_Turn)
    {
        int x = (int)ToPosition(l_Index).x;
        int z = (int)ToPosition(l_Index).z;
        //味方ターン
        if (l_Turn == Turn.PLAYERTURN)
        {
            //上
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
        //敵ターン
        else if (l_Turn == Turn.ENEMYTURN)
        {
            //上
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
    /// 右側のゴールに一番近いガイスターを探して、インデックスを返す
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
    /// 左側のゴールに一番近いガイスターを探して、インデックスを返す
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
    /// プレイヤーの駒のインデックス一覧をリストにまとめて返す
    /// </summary>
    /// <param name="array">見る配列</param>
    /// <returns>プレイヤーの駒のインデックス一覧</returns>
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
    /// 色指定すると決められた色だけ取得
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
    /// 敵の駒のインデックス一覧をリストにまとめて返す
    /// </summary>
    /// <param name="array">見る配列</param>
    /// <returns>敵の駒のインデックス一覧</returns>
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
    /// 勝敗が決まっているかどうか確認
    /// 配列1が勝敗決まっているかどうか
    /// 配列2がどちらが勝ったか(true=Player,false=Enemy)
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
            //プレイヤーの赤が無くなる、もしくは敵の青がなくなる
            l_ClearFrag[0] = true;
            l_ClearFrag[1] = true;
        }
        else
        if(FindPlayerGeister(l_ArrayBoard, (int)BoardValue.PlayerBlue).Count==0||
           FindPlayerGeister(l_ArrayBoard, (int)BoardValue.EnemyRed).Count == 0)
        {
            //敵の赤が無くなる、もしくはプレイヤーの青がなくなる
            l_ClearFrag[0] = true;
            l_ClearFrag[1] = false;
        }
        else
        {
            //ゴールしている
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
