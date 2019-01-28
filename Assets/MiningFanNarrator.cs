using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningFanNarrator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
            NarrationManager.instance.PlayClip(2);
    }
}
