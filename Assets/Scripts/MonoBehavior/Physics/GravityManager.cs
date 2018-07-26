using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GravityManager : MonoBehaviour
{

    private static GravityManager _instance;

    public static GravityManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GravityManager>();
            return _instance;
        }
    }   
    
    public Vector2 direction
    {
        get
        {
            return Physics2D.gravity.normalized;
        }
    }

    public void ChangeGravityDirection()
    {
        Physics2D.gravity = -Physics2D.gravity;
    }

    public void ChangeGravityDirectionTo(float gravity)
    {
        Physics2D.gravity = gravity*direction;
    }

}
