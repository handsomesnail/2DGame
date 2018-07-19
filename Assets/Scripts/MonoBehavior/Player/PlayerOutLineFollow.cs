using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutLineFollow : MonoBehaviour
{
    public SpriteRenderer followSpriteRender;
    private SpriteRenderer thisSpriteRender;

    private void Awake()
    {
        thisSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        thisSpriteRender.sprite = followSpriteRender.sprite;
        thisSpriteRender.flipY = followSpriteRender.flipY;
        thisSpriteRender.flipX = followSpriteRender.flipX;
    }

}
