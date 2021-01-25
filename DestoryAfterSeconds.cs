using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private int _secondsToWait;
    
    void Start()
    {
        StartCoroutine(DestorySelf());
    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(_secondsToWait);
        Destroy(gameObject);
    }

}
