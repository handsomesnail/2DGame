using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1PassTrigger : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            //游戏通关
            StartCoroutine(SceneManager.Instance.ConvertSceneAsync("MainScene"));
        }
    }

}
