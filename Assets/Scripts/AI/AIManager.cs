using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    /// <summary>
    /// AIが探索、予測に使う配列
    /// </summary>
    public int[] _aiArrayBoard = null;

    /// <summary>
    /// 現在の配列のコピー
    /// </summary>
    /// <param name="ArrayBoard"></param>
    public void SetNowBoard(int[] ArrayBoard)
    {
        _aiArrayBoard = ArrayBoard;
    }
}
