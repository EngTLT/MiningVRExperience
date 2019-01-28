using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public static HubManager instance;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        StartCoroutine(StartExperience());
    }
    IEnumerator StartExperience()
    {
        yield return new WaitForSeconds(2);
        NarrationManager.instance.PlayClip(0);
    }
}
