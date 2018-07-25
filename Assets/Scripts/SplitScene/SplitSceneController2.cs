using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

//必须正交摄像机
//需要背景图加上碰撞体
[RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
public class SplitSceneController2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler {

    public bool allowExchange = true; //是否允许交换
    private Vector3 staticPos; //标准位置 用于交换
    private float index;//距离相机距离(Z轴)

    public new Collider2D collider2D;
    private Vector3 BeginDragLocalPos;
    private Vector3 OnDragLocalPos;

    private static int defaultLayerID = 0;
    private static int sceneDragLayerId = 0;
    int a = 0;

    private void Awake() {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        staticPos = transform.position;
        index = transform.position.z;
        if(sceneDragLayerId == 0) {
            sceneDragLayerId = SortingLayer.NameToID("DragedScene");
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        BeginDragLocalPos = eventData.pointerCurrentRaycast.worldPosition;
        collider2D.enabled = false;
        //transform.SetAsLastSibling();
        IncreaseSortLayer(transform);
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
        GameObject dropGameObject = eventData.pointerCurrentRaycast.gameObject;
        if (dropGameObject != null && dropGameObject.CompareTag("SplitScene") && dropGameObject != this.gameObject) {
            Vector3 tempPos = dropGameObject.transform.position;
            SplitSceneController2 dropSplitSceneController2 = dropGameObject.GetComponent<SplitSceneController2>();
            Vector3 tempStaicPos = dropSplitSceneController2.staticPos;

            dropSplitSceneController2.collider2D.enabled = false;
            dropGameObject.transform.DOMove(staticPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                dropSplitSceneController2.staticPos = staticPos;
                dropSplitSceneController2.collider2D.enabled = true;
            });

            this.collider2D.enabled = false;
            this.transform.DOMove(tempStaicPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                this.staticPos = tempStaicPos;
                this.collider2D.enabled = true;
                DecreaseSortLayer(transform);
            });
        }
        else {
            this.collider2D.enabled = false;
            this.transform.DOMove(staticPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                this.collider2D.enabled = true;
                DecreaseSortLayer(transform);
            });
        }
    }

    public void OnDrop(PointerEventData eventData) {

    }

    public void OnPointerClick(PointerEventData eventData) {
        InputManager.Instance.BackgroundRaycaster.enabled = true;
        collider2D.enabled = false;
        //相机切到该场景的全景
        //关闭collider
    }

    /// <summary>改变某个物体及其所有子物体的渲染层级</summary>
    private void ChangeSortLayer(Transform transform, bool isIncreased) {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null) {
            if(isIncreased)
                renderer.sortingLayerID = sceneDragLayerId;
            else renderer.sortingLayerID = defaultLayerID;
        }
        foreach(Transform child in transform) {
            ChangeSortLayer(child, isIncreased);
        }
    }

    /// <summary>减小某个物体及其所有子物体的渲染层级</summary>
    private void DecreaseSortLayer(Transform transform) {
        ChangeSortLayer(transform, false);
    }

    /// <summary>提高某个物体及其所有子物体的渲染层级</summary>
    private void IncreaseSortLayer(Transform transform) {
        ChangeSortLayer(transform, true);
    }

}
