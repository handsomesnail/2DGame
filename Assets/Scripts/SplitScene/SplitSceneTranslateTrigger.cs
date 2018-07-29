using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using System.Linq;

[RequireComponent(typeof(Collider2D))]
public class SplitSceneTranslateTrigger : MonoBehaviour {

    public int targetlevelPosIndex;
    public Transform targetPos;

    public Collider BoundingVolume;

    public UnityEvent OnTranslated;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            SplitSceneController2 targetSSC = SplitSceneController2.splitScenes.Where(ssc => ssc.levelPosIndex == targetlevelPosIndex).First();
            collider.transform.position = targetPos.position;
            PlayerData.Instance.levelIndex = targetSSC.levelIndex;
            PlayerData.Player.transform.SetParent(targetSSC.transform, true);
            CameraSwitch.Instance.followCamera.GetComponent<CinemachineConfiner>().m_BoundingVolume = BoundingVolume;
            OnTranslated.Invoke();
        }
    }

}
