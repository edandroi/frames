using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAudio : MonoBehaviour
{
    public AudioClip goalAudio;
    private AudioSource m_AudioSource;

    public bool playSound = false;
    
    void Start()
    {
        m_AudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (playSound)
        {
            m_AudioSource.PlayOneShot(goalAudio);
            playSound = false;
        }
    }
}
