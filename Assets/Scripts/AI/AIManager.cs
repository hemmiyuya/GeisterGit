using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    /// <summary>
    /// AI���T���A�\���Ɏg���z��
    /// </summary>
    public int[] _aiArrayBoard = null;

    /// <summary>
    /// ���݂̔z��̃R�s�[
    /// </summary>
    /// <param name="ArrayBoard"></param>
    public void SetNowBoard(int[] ArrayBoard)
    {
        _aiArrayBoard = ArrayBoard;
    }
}
