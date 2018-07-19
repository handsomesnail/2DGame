using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public Vector2 gravity = new Vector2(0, -9.8f);    
    
    public Vector2 direction
    {
        get
        {
            return gravity.normalized;
        }
    }

    public void ChangeGravityDirection()
    {
        Instance.gravity = -Instance.gravity;
    }
}
