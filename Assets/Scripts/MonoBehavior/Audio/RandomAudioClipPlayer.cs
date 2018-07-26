using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioClipPlayer : MonoBehaviour
{
    public AudioClip[] clips;

    public bool randomizePitch = false;
    public float pitchRange = 0.2f;

    protected AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayerRandomSound()
    {
        AudioClip[] source = clips;

        int index = Random.Range(0, source.Length);

        if (randomizePitch)
            _source.pitch = Random.Range(1.0f - pitchRange, 1.0f + pitchRange);

        _source.PlayOneShot(source[index]);
    }
}
