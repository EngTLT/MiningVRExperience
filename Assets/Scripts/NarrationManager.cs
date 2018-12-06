using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NarrationManager : MonoBehaviour
{
    public static NarrationManager instance;
    public AudioClip[] narrationClips;

    AudioSource player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        player = GetComponent<AudioSource>();
        player.playOnAwake = false;
    }

    public void PlayClip(int clipIndex)
    {
        if(clipIndex < narrationClips.Length && clipIndex >= 0)
        {
            player.clip = narrationClips[clipIndex];
            player.Play();
        }
    }

    public void PlayClip(int clipIndex, float delay)
    {
        if (clipIndex < narrationClips.Length && clipIndex >= 0)
        {
            StartCoroutine(ClipDelay(narrationClips[clipIndex], delay));
        }
    }

    IEnumerator ClipDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        player.clip = clip;
    }
}