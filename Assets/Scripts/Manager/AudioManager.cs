using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    private AudioSource audioSource;

    private void OnEnable(){

        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name){
        if(this.audioSource.isPlaying)
        {
            return;
        }

        int soundIndex = -1;

        soundIndex = this.SearSoundIndex(name);

        if(soundIndex == -1)
        {
            throw new ArgumentException("¡ARGUMENTO INVÁLIDO!");
        }

        this.audioSource.clip = this.audioClips[soundIndex];

        this.audioSource.Play();
    }

        public void PlaySound(string name, float volume){
        if(this.audioSource.isPlaying)
        {
            return;
        }

        int soundIndex = -1;

        soundIndex = this.SearSoundIndex(name);

        if(soundIndex == -1)
        {
            throw new ArgumentException("¡ARGUMENTO INVÁLIDO!");
        }

        this.audioSource.clip = this.audioClips[soundIndex];

        this.audioSource.volume = volume;

        this.audioSource.Play();
    }

    public void StopSound()
    {
        this.audioSource.Stop();
    }

    private int SearSoundIndex(string name)
    {
        for(int i = 0; i < this.audioClips.Length; i++)
        {
            if(this.audioClips[i].name == name)
            {
                return i;
            }
        }

        return -1;
    }
}
