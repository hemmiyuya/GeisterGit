using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�Ǘ��X�N���v�g
/// </summary>
public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// �z��I�u�W�F�N�g
    /// </summary>
    private GameObject _boardObj;

    /// <summary>
    /// �z��Ǘ��X�N���v�g
    /// </summary>
    private BoardManager _boardManager;

    /// <summary>
    /// AI�̊Ǘ�
    /// </summary>
    private AIManager _aiManager;

    /// <summary>
    /// AI�̃K�C�X�^�[�������X�N���v�g
    /// </summary>
    private AIGeisterMove _aiMove;

    /// <summary>
    /// �G�̃K�C�X�^�[
    /// </summary>
    private GameObject _enemyGeister;

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

    private void Awake()
    {
        _aiManager = GameObject.FindGameObjectWithTag("AIManager").GetComponent<AIManager>();
        _boardObj = GameObject.FindGameObjectWithTag("Board");
        _boardManager = _boardObj.GetComponent<BoardManager>();
    }

    /// <summary>
    /// �G�̃^�[��
    /// </summary>
    public void EnemyTurn()
    {
        //�G�̍s������
        //�z��̃R�s�[
        _aiManager.SetNowBoard(_boardManager.ArrayBoard);


        //�^�[���I��

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

        if (_residueBlueGeister == 0)
        {
            //�v���C���[����
        }
        if (_residueRedGeister == 0)
        {
            //�G����
        }

    }

    /// <summary>
    /// �K�C�X�^�[�u��
    /// </summary>
    public IEnumerator SetGeister()
    {
        Debug.Log("�Z�b�g�K�C�X�^�[");
        //�z�u����z��ԍ��͈̔�
        int minValue1 = 25;
        int maxValue1 = 28;
        List<int> randomList1 = new List<int>();

        int minValue2 = 31;
        int maxValue2 = 34;
        List<int> randomList2 = new List<int>();

        int colorMinValue = 0;
        int colorMaxValue = 7;
        List<int> randomColorList = new List<int>();

        //��i�ڃ��X�g
        for(int i = minValue1; i <= maxValue1; i++)
        {
            randomList1.Add(i);
        }
        //��i�ڃ��X�g
        for(int i = minValue2; i <= maxValue2; i++)
        {
            randomList2.Add(i);
        }
        //�F���X�g
        for(int i = colorMinValue; i <= colorMaxValue; i++)
        {
            if (i <= 3)
            {
                randomColorList.Add(1);
            }
            else
            {
                randomColorList.Add(2);
            }
        }

        //�d�����Ȃ��悤�ɔz�u����
        while (randomList2.Count > 0)
        {
            //�����擾
            int randomIndex = Random.Range(0, randomList1.Count);
            int randomValue = randomList1[randomIndex];
            randomList1.RemoveAt(randomIndex);
            //�F����
            int randomColorIndex = Random.Range(0, randomColorList.Count);
            int randomColorValue = randomColorList[randomColorIndex];
            randomColorList.RemoveAt(randomColorIndex);
            //���W�ϊ�
            Vector3 randomvector3 = _boardManager.ToPosition(randomValue);
            if (randomColorValue == 1) 
            { 
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterBlue("Enemy");
            }
            else if (randomColorValue == 2)
            {
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterRed("Enemy");
            }
            
            //��i�ڂ������悤��
            randomIndex= Random.Range(0, randomList2.Count);
            randomValue = randomList2[randomIndex];
            randomList2.RemoveAt(randomIndex);
            randomColorIndex = Random.Range(0, randomColorList.Count);
            randomColorValue = randomColorList[randomColorIndex];
            randomColorList.RemoveAt(randomColorIndex);

            randomvector3 = _boardManager.ToPosition(randomValue);
            if (randomColorValue == 1)
            {
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterBlue("Enemy");
            }
            else if (randomColorValue == 2)
            {
                _boardManager.GetPixel((int)randomvector3.x, (int)randomvector3.z).GetComponent<Pixel>().SetGeisterRed("Enemy");
            }

            yield return new WaitForFixedUpdate();
        }
        yield break;
        
    }

    /// <summary>
    /// �S�[������
    /// </summary>
    public void Goal()
    {

    }
}
