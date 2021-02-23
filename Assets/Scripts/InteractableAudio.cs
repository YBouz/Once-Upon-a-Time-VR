using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAudio : Interactable
{
    public AudioSource audio;

    public override void Pressed()
    {
        //audio = GetComponent<AudioSource>();
        audio.enabled = !audio.enabled;
    }
}
