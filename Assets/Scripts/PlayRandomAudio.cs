using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] soundtrackList;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = soundtrackList[Random.Range(0, soundtrackList.Length)];
        audioSource.Play();
    }
}
