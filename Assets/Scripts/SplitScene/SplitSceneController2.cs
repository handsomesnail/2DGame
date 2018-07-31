using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using System;
using Cinemachine;

//最好正交摄像机
//需要背景图加上碰撞体
[RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
public class SplitSceneController2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler {

    public int levelIndex;
    public int levelPosIndex;
    public bool allowExchange = true; //是否允许交换
    public bool allowEnter = true; //点击进入是否激活
    private Vector3 staticPos; //标准位置 用于交换
    private float index;//距离相机距离(Z轴)
    public GameObject outline;
    public Collider DeadZone;

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
            outline.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
            return;
        }
        BeginDragLocalPos = eventData.pointerCurrentRaycast.worldPosition;
        collider2D.enabled = false;
        //transform.SetAsLastSibling();
        ChangeSortLayer(transform, true);
        transform.position += new Vector3(0, 0, -5);
        index -= 5;
        outline.GetComponent<SpriteRenderer>().DOFade(0.8f, 0.5f);
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
            outline.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
            return;
        }
        collider2D.enabled = true;
        index += 5;
        outline.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        GameObject dropGameObject = eventData.pointerCurrentRaycast.gameObject;
        SplitSceneController2 dropSplitSceneController2 = dropGameObject.GetComponent<SplitSceneController2>();
        if (dropGameObject != null && dropGameObject.CompareTag("SplitScene") && dropGameObject != this.gameObject && dropSplitSceneController2.allowExchange) {
            Vector3 tempPos = dropGameObject.transform.position;
            Vector3 tempStaicPos = dropSplitSceneController2.staticPos;

            //互换标准位置
            dropSplitSceneController2.collider2D.enabled = false;
            dropGameObject.transform.DOMove(staticPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                dropSplitSceneController2.staticPos = staticPos;
                if (InputManager.Instance.isSplit) {
                    dropSplitSceneController2.collider2D.enabled = true;
                }
            });

            this.collider2D.enabled = false;
            this.transform.DOMove(tempStaicPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                this.staticPos = tempStaicPos;
                ChangeSortLayer(transform, false);
                if (InputManager.Instance.isSplit) {
                    this.collider2D.enabled = true;
                }
            });

            //互换位置索引和DeadZone
            Utility.Exchange(ref this.levelPosIndex, ref dropSplitSceneController2.levelPosIndex);
            Utility.Exchange(ref this.DeadZone, ref dropSplitSceneController2.DeadZone);
        }
        else {
            this.collider2D.enabled = false;
            this.transform.DOMove(staticPos, 1.0f).SetEase(Ease.OutQuart).OnComplete(() => {
                if (InputManager.Instance.isSplit) {
                    this.collider2D.enabled = true;
                }
                ChangeSortLayer(transform, false);
            });
        }
    }

    public void OnDrop(PointerEventData eventData) {

    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.dragging)
            return;
        if (!allowEnter) {
            return;
        }
        if(PlayerData.Instance.levelIndex != levelIndex) {
            return;
        }
        InputManager.Instance.isSplit = false;
        CameraSwitch.Instance.followCamera.GetComponent<CinemachineConfiner>().m_BoundingVolume = DeadZone;
        Debug.Log("进入单屏");
        OnEnterIntoSpiltScene();
        OnCameraEnterScene.Invoke();
        PlayerData.Player.GetComponent<PlayerController>().enabled = true;
        PlayerData.Player.GetComponent<Collider2D>().enabled = true;
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

    private Dictionary<Transform, int> object_Layer_dic = new Dictionary<Transform, int>();
    private void ChangeSortLayer(Transform transform, bool increased) {
        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null)
            renderer.sortingLayerID = increased ? sceneDragSortLayerId : defaultSortLayerID;

        if (increased) {
            object_Layer_dic[transform] = transform.gameObject.layer;
            transform.gameObject.layer = sceneDragLayerIndex;
        }
        else {
            try {
                transform.gameObject.layer = object_Layer_dic[transform];
            }
            catch (Exception) {
                Debug.LogError(transform.gameObject.name);
            }
            object_Layer_dic.Remove(transform);
        }
        foreach (Transform child in transform) {
            ChangeSortLayer(child, increased);
        }
    }

}
