using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera wholeSceneCamera;
    public CinemachineVirtualCamera followCamera;

    public UnityEvent OnSwitchToFollowCamera;
    public UnityEvent OnSwitchToWholeSceneCamera;

    public void SwitchToWholeCamera()
    {
        wholeSceneCamera.Priority = followCamera.Priority + 1;
        OnSwitchToWholeSceneCamera.Invoke();
    }

    public void SwitchToFollowCamera()
    {
        followCamera.Priority = wholeSceneCamera.Priority + 1;
        OnSwitchToFollowCamera.Invoke();
    }

}
