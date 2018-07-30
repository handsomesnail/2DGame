using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {
    NULL = 0, //无道具
    Stick //撬棍
}

public class ItemManager : MonoBehaviour {

    public static ItemManager Instance {
        get; private set;
    }
    public ItemType currentItem = ItemType.NULL;
    private ItemPickupTrigger triggerItem = null;//当前接触的Item

    public MobileButton PickUpButton;
    public MobileButton ExchangeButton;
    public MobileButton UseItemButton;

    //默认状态 (itemType-1)*2 高亮状态 (itemType-1)*2+1
    public Sprite[] ItemButtonSprites;

    private void Awake() {
        Instance = this;
    }

    /// <summary>拾取(交换)道具</summary>
    public void PickUpItem() {
        if (triggerItem == null) {
            throw new System.Exception("当前没有接触道具");
        }
        int itemType = (int)triggerItem.itemType;
        PickUpButton.gameObject.SetActive(false);
        ExchangeButton.gameObject.SetActive(false);
        (UseItemButton.targetGraphic as Image).sprite = ItemButtonSprites[(itemType - 1) * 2 +1];
        UseItemButton.spriteState = new SpriteState() { disabledSprite = ItemButtonSprites[(itemType - 1) * 2] };
        UseItemButton.gameObject.SetActive(true);
        UseItemButton.interactable = false;
        currentItem = (ItemType)itemType;
        Destroy(triggerItem.gameObject);
    }

    /// <summary>进入道具边界</summary>
    public void EnterItem(ItemPickupTrigger itemTrigger) {
        triggerItem = itemTrigger;
        UseItemButton.gameObject.SetActive(false);
        PickUpButton.gameObject.SetActive(false);
        ExchangeButton.gameObject.SetActive(false);
        if (currentItem == ItemType.NULL) {
            PickUpButton.gameObject.SetActive(true);
            PickUpButton.interactable = true;
        }
        else {
            ExchangeButton.gameObject.SetActive(true);
            ExchangeButton.interactable = true;
        }
    }

    /// <summary>离开道具边界</summary>
    public void ExitItem(ItemPickupTrigger itemTrigger) {
        triggerItem =null;
        UseItemButton.gameObject.SetActive(false);
        PickUpButton.gameObject.SetActive(false);
        ExchangeButton.gameObject.SetActive(false);
        if (currentItem == ItemType.NULL) {
            PickUpButton.gameObject.SetActive(true);
            PickUpButton.interactable = false;
        }
        else {
            UseItemButton.gameObject.SetActive(true);
            UseItemButton.interactable = false;
        }
    }

    /// <summary>进入道具触发地</summary>
    public void EnterItemTrigger() {
        UseItemButton.interactable = true;
    }

    /// <summary>离开道具触发地 </summary>
    public void ExitItemTrigger() {
        UseItemButton.interactable = false;
    }



}
