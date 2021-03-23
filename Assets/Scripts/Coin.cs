using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コインスクリプト
/// </summary>
public class Coin : MonoBehaviour
{
    private const int MinPower = 12000;
    private const int MaxPower = 20000;
    private GameObject _addPowerObj;
    private GameObject _collisionCheckObj;

    private GameObject _canvasObj;

    private GameManager _gameManager;

    private void Awake()
    {
        _addPowerObj = gameObject.transform.GetChild(0).gameObject;
        _collisionCheckObj = transform.GetChild(1).gameObject;
        _canvasObj = GameObject.FindGameObjectWithTag("Canvas");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void StartAddPowor()
    {
        StartCoroutine(AddPowor());
    }

    public IEnumerator AddPowor()
    {
        _addPowerObj.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(MinPower, MaxPower));
        _canvasObj.transform.GetChild(1).gameObject.SetActive(false);

        yield return new WaitForSeconds(0.8f);

        _collisionCheckObj.GetComponent<CoinCollisinCheck>()._checkNow = true;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// コインの裏表チェック
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckFace()
    {
        Debug.Log("1");
        float t = 0;
        while (t< 1.5f)
        {       
            t += (Time.deltaTime);
            Debug.Log(t + "t");
            yield return new WaitForFixedUpdate();
        }
        
        Debug.Log("2");
        //表
        Debug.Log(transform.rotation.eulerAngles.x + "x");
        if (transform.rotation.eulerAngles.x == 270)
        {
            Debug.Log("表");
            StartCoroutine(_gameManager.SetAttackOrder(true));
        }
        //裏
        else if (transform.rotation.eulerAngles.x == 90)
        {
            Debug.Log("裏");
            StartCoroutine(_gameManager.SetAttackOrder(false));
        }
        else
        {
            Debug.Log("aaaa");
        }

        yield break;

    }
}
