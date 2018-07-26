using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTest : MonoBehaviour
{
    public Transform originPosition;
    public Transform targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.position = targetPosition.position;
            SwitchPosition();
        }
    }

    private void SwitchPosition()
    {
        Transform temp = originPosition;
        originPosition = targetPosition;
        targetPosition = temp;
    }
}
