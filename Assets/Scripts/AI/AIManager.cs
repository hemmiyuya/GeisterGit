using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    /// <summary>
    /// AIªTõA\ªÉg¤zñ
    /// </summary>
    public int[] _aiArrayBoard = null;

    /// <summary>
    /// »ÝÌzñÌRs[
    /// </summary>
    /// <param name="ArrayBoard"></param>
    public void SetNowBoard(int[] ArrayBoard)
    {
        _aiArrayBoard = ArrayBoard;
    }
}
