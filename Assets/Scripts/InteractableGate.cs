using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGate : Interactable
{
    public GameObject player, events, pointer, reticle;
    public Transform pos1, pos2, pos3;
    public float speed;
    public float timer, timer2, timer3;

    public GameObject khalil, teller;
    public AudioSource gramophone;

    int isPressed = 0;
    

    void Update()
    {

        if (isPressed > 0) {
            
            //move to pos1
            if(isPressed == 1) {
                player.transform.position = Vector3.MoveTowards(player.transform.position, pos1.position, speed * Time.deltaTime);
            }
            
            //if reached pos1, move to pos2
            if(isPressed == 2) {
                player.transform.position = Vector3.MoveTowards(player.transform.position, pos2.position, speed * Time.deltaTime);
            }

            //if reached pos2, move to pos3
            if(isPressed == 3) {
                player.transform.position = Vector3.MoveTowards(player.transform.position, pos3.position, speed * Time.deltaTime);
            }
        }
    }

    public override void Pressed()
    {
        events.SetActive(!events.activeInHierarchy);
        pointer.SetActive(!pointer.activeInHierarchy);
        reticle.SetActive(!reticle.activeInHierarchy);

        //disable clips and audio while moving
        khalil.SetActive(false);
        teller.SetActive(false);
        gramophone.enabled = false;

        Invoke("first", 0);
        Invoke("second", timer);
        Invoke("third", timer2);
        Invoke("enablePointer", timer3 - 1);
        Invoke("notPressed", timer3);
    }

    public void first()
    {
        isPressed = 1;
    }

    public void second()
    {
        isPressed = 2;
    }

    public void third()
    {
        isPressed = 3;
    }

    public void enablePointer()
    {
        events.SetActive(!events.activeInHierarchy);
        pointer.SetActive(!pointer.activeInHierarchy);
        reticle.SetActive(!reticle.activeInHierarchy);
    }

    public void notPressed()
    {
        isPressed = 0;
    }

}