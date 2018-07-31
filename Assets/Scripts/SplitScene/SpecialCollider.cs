using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollider : MonoBehaviour {

    [Header("屏蔽特殊碰撞体")]
    public Collider2D specialCollider;
    public Collider2D specialCollider2;

    public static SpecialCollider Instance {
        get;private set;
    }

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
