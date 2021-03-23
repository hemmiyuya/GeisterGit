using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コインが地面についたことの判定
/// </summary>
public class CoinCollisinCheck : MonoBehaviour
{
    public bool _checkNow { get; set; } = false;

    private void Awake()
    {
        _checkNow = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (_checkNow && other.tag == "Board")
        {
            StartCoroutine(transform.parent.GetComponent<Coin>().CheckFace());
            _checkNow = false;
        }
    }
}
