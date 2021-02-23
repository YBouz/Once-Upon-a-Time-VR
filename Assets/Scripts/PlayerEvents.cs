using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour {
    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion

    #region Anchors
    public GameObject leftAnchor;
    public GameObject rightAnchor;
    public GameObject headAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> controllerSets = null;
    private OVRInput.Controller inputSource = OVRInput.Controller.None;
    private OVRInput.Controller controller = OVRInput.Controller.None;
    private bool inputActive = true;
    #endregion

    private void Awake() {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        controllerSets = CreateControllerSets();
    }

    void Start()
    {
        //UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 1.5f;
        //OVRManager.display.displayFrequency = 72.0f;
        //Oculus.Platform.Core.Initialize();
    }

    private void OnDestroy() {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    // Update is called once per frame
    private void Update() {
        //check for active input
        if(!inputActive) {
            return;
        }

        //check if controller exits
        CheckForController();

        //check for input source
        CheckInputSource();

        //check for actual input
        Input();
    }

    private void CheckForController() {
        OVRInput.Controller controllerCheck = controller;

        //left remote
        if(OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote)) {
            controllerCheck = OVRInput.Controller.LTrackedRemote;
        }

        //right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)) {
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        }

        //no controllers --> headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote) && !OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)) {
            controllerCheck = OVRInput.Controller.Touchpad;
        }

        //update
        controller = UpdateSource(controllerCheck, controller);
    }

    private void CheckInputSource() {
        inputSource = UpdateSource(OVRInput.GetActiveController(), inputSource);
    }

    private void Input() {
        //trigger down (clicked) 
        //OVRInput.Button.PrimaryTouchpad if I wanna use touchpad click
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) { 
            if (OnTouchpadDown != null) {
                OnTouchpadDown();
            }
        }

        //trigger up (released)
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
            if(OnTouchpadUp != null) {
                OnTouchpadUp();
            }
        }
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous) {
        //check if equal values
        if (check == previous)
            return previous;

        //get controller object
        GameObject controllerObj = null;
        controllerSets.TryGetValue(check, out controllerObj);

        //if no controller, set to head
        if(controllerObj == null) 
            controllerObj = headAnchor;

        //send event 
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObj);

        //return new value
        return check;
    }

    private void PlayerFound() {
        inputActive = true;
    }

    private void PlayerLost() {
        inputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets() {
        Dictionary<OVRInput.Controller, GameObject> newSet = new Dictionary<OVRInput.Controller, GameObject>() 
        {   {OVRInput.Controller.LTrackedRemote, leftAnchor},
            {OVRInput.Controller.RTrackedRemote, rightAnchor},
            {OVRInput.Controller.Touchpad, headAnchor}
        };
        return newSet;
    }


}
