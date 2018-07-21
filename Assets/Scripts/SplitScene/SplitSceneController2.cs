using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//必须正交摄像机
//需要背景图加上碰撞体
[RequireComponent(typeof(Renderer),typeof(Collider2D)), DisallowMultipleComponent]
public class SplitSceneController2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler {

    public bool allowExchange = true; //是否允许交换
    private Vector3 staticPos; //标准位置 用于交换
    private float index;//距离相机距离(Z轴)

    private new Collider2D collider2D;
    private new Renderer renderer;
    private Vector3 BeginDragLocalPos;
    private Vector3 OnDragLocalPos;

    private void Awake() {
        collider2D = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        staticPos = transform.position;
        index = transform.position.z;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        BeginDragLocalPos = eventData.pointerCurrentRaycast.worldPosition;
        collider2D.enabled = false;
        renderer.sortingOrder = 1;
        Debug.Log(BeginDragLocalPos);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        if (eventData.pointerCurrentRaycast.worldPosition != Vector3.zero) {
            Vector3 targetPos = staticPos + (eventData.pointerCurrentRaycast.worldPosition - BeginDragLocalPos);
            transform.position = new Vector3(targetPos.x, targetPos.y, index);
        }
        else transform.position = staticPos;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        collider2D.enabled = true;
        renderer.sortingOrder = 0;
        GameObject dropGameObject = eventData.pointerCurrentRaycast.gameObject;
        if (dropGameObject != null) {
            if (dropGameObject.CompareTag("SplitScene") && dropGameObject != this.gameObject) {
                Vector3 tempPos = dropGameObject.transform.position;
                SplitSceneController2 dropSplitSceneController2 = dropGameObject.GetComponent<SplitSceneController2>();
                Vector3 tempStaicPos = dropSplitSceneController2.staticPos;

                dropGameObject.transform.position = staticPos;
                dropSplitSceneController2.staticPos = staticPos;
                this.transform.position = tempStaicPos;
                this.staticPos = tempStaicPos;
            }
            else transform.position = staticPos;
        }
        else transform.position = staticPos;
    }

    public void OnDrop(PointerEventData eventData) {

    }

    public void OnPointerClick(PointerEventData eventData) {
        //相机切到该场景的全景
    }
}
