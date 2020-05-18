using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRInputController : MonoBehaviour {

    enum ControllerType { LeftController, RightController };
    [SerializeField] ControllerType cType;
    UnityEngine.XR.InputDevice controller;

    //bools for event triggers
    bool hasTrigger, hasPrimary, hasSecondary, hasGrip;

    //values of buttons
    bool trigger, buttonPrimary, buttonSecondary, grip;

    HapticCapabilities capabilities;


    void Start() {

    }


    void Update() {
        AssignControllers();



        if (controller != null) {
            //trigger down event
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out trigger) && trigger && !hasTrigger) {
                print("Trigger down");
                hasTrigger = true;

                controller.SendHapticImpulse(0, .2f, .1f);
            }
            //trigger up event
            if (!trigger && hasTrigger) {
                print("Trigger up");
                hasTrigger = false;

                
            }


            //grip down event
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out grip) && grip && !hasGrip) {
                print("Grip Down");
                hasGrip = true;

            }
            //grip up event
            if (!grip && hasGrip) {
                print("Grip Up");
                hasGrip = false;
            }


            //joystick
            Vector2 joystick;
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out joystick) && joystick.sqrMagnitude > 0f) {
                print("joystick value: " + joystick);
            }

            //button primary down event
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonPrimary) && buttonPrimary && !hasPrimary) {
                print("Primary down");
                hasPrimary = true;
            }
            //button primary up event
            if (!buttonPrimary && hasPrimary) {
                print("Primary up");
                hasPrimary = false;
            }

            //button secondary down event
            if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out buttonSecondary) && buttonSecondary && !hasSecondary) {
                print("Secondary Down");
                hasSecondary = true;
            }
            //button secondary up event
            if (!buttonSecondary && hasSecondary) {
                print("Secondary Up");
                hasSecondary = false;
            }





        }
    }


    private void AssignControllers() {
        if (cType == ControllerType.LeftController) {
            var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
            if (leftHandDevices.Count == 1) controller = leftHandDevices[0];
        } else if (cType == ControllerType.RightController) {
            var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
            if (rightHandDevices.Count == 1) controller = rightHandDevices[0];
        }

        controller.TryGetHapticCapabilities(out capabilities);
    }

    

}
