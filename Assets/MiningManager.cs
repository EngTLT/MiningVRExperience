using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartMining());
    }

    IEnumerator StartMining()
    {
        yield return new WaitForSeconds(2);
        NarrationManager.instance.PlayClip(0);
    }
}
