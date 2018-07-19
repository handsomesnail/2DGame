using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Animator animator;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))
            animator.SetTrigger("Climb");            
	}

    public void ClimbEnd()
    {
        transform.position += new Vector3(1, 0, 0);
    }
}
