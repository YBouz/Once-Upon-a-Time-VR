using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour {
    public Pointer pointer;
    public SpriteRenderer circleRenderer;

    public LineRenderer lineRenderer;
    //public Sprite openSprite;
    //public Sprite closedSprite;

    private Camera camera = null;
    
    // my additions 
    private AudioSource audio;
    private Behaviour halo;
    private ParticleSystem ps;

    private void Awake() {
        //subscribe
        pointer.onPointerUpdate += UpdateSprite;

        //get camera
        camera = Camera.main;
    }
    // Update is called once per frame
    private void Update() {
        transform.LookAt(camera.gameObject.transform);
    }

    private void OnDestroy() {
        //unsubscribe
        pointer.onPointerUpdate -= UpdateSprite;
    }

    private void UpdateSprite(Vector3 point, GameObject hitObject) {
        transform.position = point;
        audio = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();

        //pointing at interactable object
        if (hitObject) {
            //reticle
            //circleRenderer.sprite = closedSprite;

            //line renderer
            lineRenderer.endColor = Color.red;

            //audio component
            audio.enabled = true;

            //halo component
            halo = (Behaviour) hitObject.GetComponent("Halo");
            halo.enabled = true;

            //particle system component
            ps.enableEmission = true;
            //ps.Emit(1);
        }

        //pointing at anything else
        else {
            //reticle
            //circleRenderer.sprite = openSprite;

            //line renderer
            lineRenderer.endColor = Color.white;

            //audio component
            audio.enabled = false;
            
            //halo component
            halo.enabled = false;

            //particle system component
            ps.enableEmission = false;
        }
    }
}
