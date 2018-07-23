using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D)),DisallowMultipleComponent]
public class Pincher : MonoBehaviour {

    [Range(10,30)]
    public float TriggerDistance = 20;

    private UnityEvent OnPinchIn = new UnityEvent();
    private UnityEvent OnPinchOut = new UnityEvent();
    private UnityEvent OnPinchEnd = new UnityEvent();

    // Subscribe to events
    void OnEnable() {
        EasyTouch.On_TouchStart2Fingers += On_TouchStart2Fingers;
        EasyTouch.On_PinchIn += On_PinchIn;
        EasyTouch.On_PinchOut += On_PinchOut;
        EasyTouch.On_PinchEnd += On_PinchEnd;
        OnPinchIn.AddListener(() => {
            if (InputManager.Instance.Interactable) {
                InputManager.Instance.OnPinchScene.Invoke();
            }
        });
    }

    void OnDisable() {
        UnsubscribeEvent();
    }

    void OnDestroy() {
        UnsubscribeEvent();
    }

    // Unsubscribe to events
    void UnsubscribeEvent() {
        EasyTouch.On_TouchStart2Fingers -= On_TouchStart2Fingers;
        EasyTouch.On_PinchIn -= On_PinchIn;
        EasyTouch.On_PinchOut -= On_PinchOut;
        EasyTouch.On_PinchEnd -= On_PinchEnd;
        OnPinchIn.RemoveAllListeners();
    }

    private void On_TouchStart2Fingers(Gesture gesture) {
        if (gesture.pickedObject == gameObject) {
            EasyTouch.SetEnableTwist(false);
            EasyTouch.SetEnablePinch(true);
        }
    }

    private void On_PinchIn(Gesture gesture) {
        if (gesture.deltaPinch > TriggerDistance) {
            OnPinchIn.Invoke();
        }
        //if (gesture.pickedObject == gameObject) {
        //    OnPinchIn.Invoke();
        //}
    }

    private void On_PinchOut(Gesture gesture) {
        OnPinchOut.Invoke();
    }

    private void On_PinchEnd(Gesture gesture) {
        OnPinchEnd.Invoke();
    }

}
