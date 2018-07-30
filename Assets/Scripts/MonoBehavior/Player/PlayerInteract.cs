using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerInteract : MonoBehaviour
{
    public SpriteRenderer playerSprite;

    private bool isFloating = false;

    private InteractOnButtonPress interactOBP;

    public void SetInteractItem(InteractOnButtonPress interact)
    {
        interactOBP = interact;
    }

    public void InteractItem()
    {
        if (interactOBP == null)
        {
            Debug.Log("null interact Object");
            return;
        }
        interactOBP.Interact();
    }

    public void EmptyInteract(InteractOnButtonPress interact)
    {
        if (interact != interactOBP)
            return;
        interactOBP = null;
    }

    #region 改变人物贴图
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

    public void FlipY()
    {
        playerSprite.flipY = playerSprite.flipY ? false : true;
    }

    public void SetisFloating()
    {
        isFloating = true;
    }
    #endregion
}
