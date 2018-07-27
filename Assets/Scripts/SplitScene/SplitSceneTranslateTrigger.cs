using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

[RequireComponent(typeof(Collider2D))]
public class SplitSceneTranslateTrigger : MonoBehaviour {

    public int targetlevelIndex;
    public Transform targetPos;

    public Collider BoundingVolume;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            collider.transform.position = targetPos.position;
            PlayerData.Instance.levelIndex = targetlevelIndex;
            CameraSwitch.Instance.followCamera.GetComponent<CinemachineConfiner>().m_BoundingVolume = BoundingVolume;
        }
    }

}
