using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

//必须正交摄像机
//需要背景图加上碰撞体
[RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
public class SplitSceneController2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler {

    public int levelIndex;
    public bool allowExchange = true; //是否允许交换
    private Vector3 staticPos; //标准位置 用于交换
    private float index;//距离相机距离(Z轴)

    public new Collider2D collider2D;
    private new Renderer renderer;
    private Vector3 BeginDragLocalPos;
    private Vector3 OnDragLocalPos;

    private static int defaultSortLayerID = 0;
    private static int sceneDragSortLayerId = 0;
    private static int defaultLayerIndex = 0;
    private static int sceneDragLayerIndex = 11;
    public static List<SplitSceneController2> splitScenes = new List<SplitSceneController2>();

    public UnityEvent OnCameraEnterScene;

    private void Awake() {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        staticPos = transform.position;
        index = transform.position.z;
        if(sceneDragSortLayerId == 0) {
            sceneDragSortLayerId = SortingLayer.NameToID("DragedScene");
        }
        splitScenes.Add(this);
    }

    private void OnDestroy() {
        splitScenes.Remove(this);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        BeginDragLocalPos = eventData.pointerCurrentRaycast.worldPosition;
        collider2D.enabled = false;
        transform.SetAsLastSibling();
        ChangeSortLayer(transform, true);
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
        SplitSceneController2 dropSplitSceneController2 = dropGameObject.GetComponent<SplitSceneController2>();
        if (dropGameObject != null && dropGameObject.CompareTag("SplitScene") && dropGameObject != this.gameObject && dropSplitSceneController2.allowExchange) {
            Vector3 tempPos = dropGameObject.transform.position;
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
                ChangeSortLayer(transform, false);
            });
        }
        else {
            this.collider2D.enabled = false;
            this.transform.DOMove(staticPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                this.collider2D.enabled = true;
                ChangeSortLayer(transform, false);
            });
        }
    }

    public void OnDrop(PointerEventData eventData) {

    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("进入单屏");
        OnCameraEnterScene.Invoke();
    }

    public void OnEnterIntoSpiltScene() {
        splitScenes.ForEach((scene) => {
            scene.collider2D.enabled = false;
        });
        InputManager.Instance.BackgroundRaycaster.enabled = false;
        if (PlayerData.Instance.levelIndex == levelIndex) {
            InputManager.Instance.Interactable = true;
        }
        else {
            InputManager.Instance.Interactable = false;
        }
    }

    private void ChangeSortLayer(Transform transform, bool increased) {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null)
            renderer.sortingLayerID = increased ? sceneDragSortLayerId : defaultSortLayerID;
        transform.gameObject.layer = increased ? sceneDragLayerIndex : defaultLayerIndex;
        foreach(Transform child in transform) {
            ChangeSortLayer(child, increased);
        }
    }



}
