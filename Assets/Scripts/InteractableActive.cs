using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video; 

public class InteractableActive : Interactable
{
    //public GameObject temp, next;
    public GameObject pop;
    public VideoPlayer video;
    
    public void Update()
    {
        if (video.isPaused) {
            video.Stop();
            pop.SetActive(!pop.activeInHierarchy);
        }
    }

    public override void Pressed()
    {
        pop.SetActive(!pop.activeInHierarchy); 
    }
}
