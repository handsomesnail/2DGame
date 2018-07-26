using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraSwitch : MonoBehaviour
{

    public static CameraSwitch Instance {
        get; private set;
    }

    private void Awake() {
        Instance = this;
    }

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
    
    public void SetPlayerFollowCameraNotFollow()
    {
        followCamera.Follow = null;
    }

}
