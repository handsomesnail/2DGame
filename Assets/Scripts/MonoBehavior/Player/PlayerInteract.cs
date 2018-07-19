using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerInteract : MonoBehaviour
{
    public Animator animator;

    public PlayerController playerController;
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))
            animator.SetTrigger("Climb");            
	}

    public void ClimbEnd()
    {
        transform.position += new Vector3(1, 0, 0);
    }

    public void StartInteract()
    {
        playerController.enabled = false;
    }

    public void EndInteract()
    {
        playerController.enabled = true;
    }
}
