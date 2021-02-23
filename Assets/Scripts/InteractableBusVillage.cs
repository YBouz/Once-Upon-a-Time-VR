using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableBusVillage : Interactable
{
    public GameObject player, events, pointer, reticle;
    public Transform moveTo;
    public int current, next;
    public float speed;
    public float timer;

    //public AudioSource audio;

    public GameObject lullaby, grinder, cooking, well, carpenter;
    
    private AsyncOperation o;

    bool isPressed = false;

    void Update()
    {
        if (isPressed) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, moveTo.position, speed * Time.deltaTime);
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

        // load next scene in the background
        o = SceneManager.LoadSceneAsync(next);
        o.allowSceneActivation = false;


        isPressed = true;
        //Invoke("playSound", timer - 3);
        Invoke("notPressed", timer);
        Invoke("changeScene", timer);
        
        
    }

    public void changeScene()
    {
        //SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(current);
        o.allowSceneActivation = true;
    }

    /*
    public void playSound()
    {
        audio.enabled = !audio.enabled;
    }*/

    public void notPressed()
    {
        isPressed = false;
    }
}