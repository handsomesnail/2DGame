using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickControl : MonoBehaviour {

	void Start () {
        StartCoroutine(WaitGuide());
	}
	

    private IEnumerator WaitGuide() {
        yield return new WaitForSeconds(60);
        transform.Find("Outline").gameObject.SetActive(true);
    }

}
