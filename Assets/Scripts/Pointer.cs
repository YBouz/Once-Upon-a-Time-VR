using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour {
    public float distance = 10f;
    public LineRenderer lineRenderer = null;
    public LayerMask everythingMask = 0;
    public LayerMask interactionMask = 0;
    public UnityAction<Vector3, GameObject> onPointerUpdate = null;

    private Transform currentOrigin = null;
    private GameObject currentObject = null;
    

    private void Awake() {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchpadDown += ProcessTouchpadDown;
    }

    private void Start() {
        SetLineColor();
    }

    private void OnDestroy() {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchpadDown -= ProcessTouchpadDown;
    }

    private void Update() {
        Vector3 hitPoint = UpdateLine();

        currentObject = UpdatePointerStatus();

        if(onPointerUpdate != null) {
            onPointerUpdate(hitPoint, currentObject);
        }
    }

    private Vector3 UpdateLine() {
        //create raycast
        RaycastHit hit = CreateRaycast(everythingMask);

        //default end
        Vector3 endPosition = currentOrigin.position + (currentOrigin.forward * distance);

        //check hit
        if(hit.collider != null) {
            endPosition = hit.point;
        }

        //set position 
        lineRenderer.SetPosition(0, currentOrigin.position);
        lineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject) {
        //set origin of pointer
        currentOrigin = controllerObject.transform;

        //check if laser is visible
        if (controller == OVRInput.Controller.Touchpad) {
            lineRenderer.enabled = false;
        } 
        else lineRenderer.enabled = true;
    }

    private GameObject UpdatePointerStatus() {
        //create ray
        RaycastHit hit = CreateRaycast(interactionMask);
        
        //check hit
        if(hit.collider) {
            return hit.collider.gameObject;
        }

        // else return
        return null;
    }

    private RaycastHit CreateRaycast(int layer) {
        RaycastHit hit;
        Ray ray = new Ray(currentOrigin.position, currentOrigin.forward);
        Physics.Raycast(ray, out hit, distance, layer);
        return hit;
    }

    private void SetLineColor() {
        if (!lineRenderer)
            return;

        Color color = Color.white;
        color.a = 0.0f; // 0.0
        lineRenderer.endColor = color;
    }

    private void ProcessTouchpadDown() {
        Interactable interactable = currentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }
    
}
