using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioSource audiosource;
    public AudioClip clip;
}


public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds; //array to the sounds class
    public static AudioManager instance; // using singleton
    private void Awake() //Awake is used to initialize any variables or game state before the game starts
    {
        instance = this;
        foreach(Sounds item in sounds)
        {
            item.audiosource = gameObject.AddComponent<AudioSource>();
            item.audiosource.clip = item.clip;
        }
    }

    public void PlayAudio(string name)
    {
        foreach(Sounds item in sounds)
        {
            item.audiosource.Stop();
        }
        Sounds sname = Array.Find(sounds, sounds => sounds.name == name);
        if (sname == null)
        {
            Debug.Log("Audio not found");
            return;
        }

        sname.audiosource.Play();
    }
   
}
