using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerInteract : MonoBehaviour
{
    public SpriteRenderer playerSprite;

    private bool isFloating = false;

    public void ChangePlayerSPFlipY()
    {
        ChangeSpriteRenderFlipY(playerSprite);
    }

    private void ChangeSpriteRenderFlipY(SpriteRenderer spriteRenderer)
    {
        if (!isFloating)
            return;
        //flipY == false 代表人物向上 flipY==true 代表人物向下
        //如果重力向上，但是人物没有翻转 就翻转Sp
        if (GravityManager.Instance.direction.y > 0 && !spriteRenderer.flipY)
            spriteRenderer.flipY = true;
        //如果重力向下，
        if (GravityManager.Instance.direction.y < 0 && spriteRenderer)
            spriteRenderer.flipY = false;
        isFloating = false;
    }

    public void SetisFloating()
    {
        isFloating = true;
    }

}
