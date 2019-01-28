using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastSiteManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartBlastSite());
    }

    IEnumerator StartBlastSite()
    {
        yield return new WaitForSeconds(2);
        NarrationManager.instance.PlayClip(0);
        while (NarrationManager.instance.GetComponent<AudioSource>().isPlaying)
            yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1);
        NarrationManager.instance.PlayClip(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        NarrationManager.instance.PlayClip(3);
    }
}
