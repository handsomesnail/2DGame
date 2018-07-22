using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadIntType
{
    public const int Fire = 0;
}

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent OnDamage;
    public UnityEvent OnDead;
    


    public void Dead()
    {

    }

}
