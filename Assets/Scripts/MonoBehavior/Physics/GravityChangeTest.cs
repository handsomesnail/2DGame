using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeTest : MonoBehaviour
{
    public PlayerController playerController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
          //  playerController.groundNormal= -playerController.groundNormal;
            GravityManager.Instance.ChangeGravityDirection();
        }
    }
}
