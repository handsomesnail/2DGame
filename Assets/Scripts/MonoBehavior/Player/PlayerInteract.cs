using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerInteract : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    
    public void ChangePlayerSPFlipY()
    {
        ChangeSpriteRenderFlipY(playerSprite);
    }

    private void ChangeSpriteRenderFlipY(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.flipY = spriteRenderer.flipY == true ? false : true;
    }
}
