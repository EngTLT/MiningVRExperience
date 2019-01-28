using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReclamationController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartReclamation());
    }

    IEnumerator StartReclamation()
    {
        yield return new WaitForSeconds(2);
        NarrationManager.instance.PlayClip(0);
        while (NarrationManager.instance.GetComponent<AudioSource>().isPlaying)
            yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2);
        NarrationManager.instance.PlayClip(1);
        while (NarrationManager.instance.GetComponent<AudioSource>().isPlaying)
            yield return new WaitForEndOfFrame();
        NarrationManager.instance.PlayClip(2);
    }
}
