using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDonkey : Interactable
{
    public GameObject player, events, pointer, reticle;
    public Transform playerPos1, playerPos2;
    public float speed;
    public float timer;

    public GameObject lullaby, grinder, cooking, well, carpenter;

    int isPressed = 0;

    void Update()
    {
        if (isPressed == 1) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerPos2.position, speed * Time.deltaTime);
        } else if (isPressed == 2) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerPos1.position, speed * Time.deltaTime);
        }
    }

    public override void Pressed()
    {
        events.SetActive(!events.activeInHierarchy);
        pointer.SetActive(!pointer.activeInHierarchy);
        reticle.SetActive(!reticle.activeInHierarchy);

        //disable clips while moving
        lullaby.SetActive(false);
        grinder.SetActive(false);
        cooking.SetActive(false);
        well.SetActive(false);
        carpenter.SetActive(false);


        Invoke("moveTo", 0);
        Invoke("enablePointer", timer - 1);
        Invoke("notPressed", timer);
    }

    public void moveTo()
    {
        if (player.transform.position == playerPos1.transform.position) {
            isPressed = 1;
        } else if (player.transform.position == playerPos2.transform.position) {
            isPressed = 2;
        }
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