using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
            NarrationManager.instance.PlayClip(3);
    }
}
