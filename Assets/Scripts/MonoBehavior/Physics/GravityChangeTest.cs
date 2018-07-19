using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeTest : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GravityManager.Instance.ChangeGravityDirection();
        }
    }
}
