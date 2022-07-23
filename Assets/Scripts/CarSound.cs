using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    AudioSource audioSource, driftAudioSource;
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip driftSound;
    [SerializeField][Range(1f,100f)] float engineSoundVolume, driftSoundVolume;
    CarController carController;
    //[SerializeField] AudioClip Idle, Gas, Break;
    [SerializeField] float audioPitchMin, audioPitchMax;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = engineSound;
        audioSource.loop = true;
        audioSource.volume = engineSoundVolume/100;
        audioSource.Play();

        driftAudioSource = gameObject.AddComponent<AudioSource>();
        driftAudioSource.clip = driftSound;
        driftAudioSource.volume = driftSoundVolume/100;
        driftAudioSource.loop = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            UpdateAudio();
    }

    private void UpdateAudio()
    {
        /*
        if (carController._HandBrake && audioSource.clip != Break)
        {
            audioSource.clip = Break;
            audioSource.Play();
        }
        else if (carController._VerticalInput > 0 && audioSource.clip != Gas)
        {
            
            audioSource.clip = Gas;
            audioSource.Play();
        }
        else if (carController._VerticalInput == 0 && audioSource.clip != Idle && audioSource.pitch == 1)
        {
            audioSource.clip = Idle;
            audioSource.Play();
        }
        else if (carController._VerticalInput < 0)
        {
            //Break Audio
        }
        */
        if (carController._isDrifting)
        {
            if (!driftAudioSource.isPlaying)
            {
                driftAudioSource.Play();
            }
        }
        else
        {
            driftAudioSource.Stop();
        } 
        float carSpeed = carController._magnitude;
        audioSource.pitch = Math.Clamp(carSpeed*3f/34, 0.3f, 3f);
    }
}
