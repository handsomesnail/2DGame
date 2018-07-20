using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform)),DisallowMultipleComponent]
public class SplitSceneController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {

    public bool allowExchange = true; //是否允许交换
    private Vector3 staticPos; //标准位置 用于交换

    //拖拽模块
    private CanvasGroup canvasGroup;
    private Vector2 BeginDragLocalPos;
    private Vector2 OnDragLocalPos;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        staticPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        transform.SetAsLastSibling();
        BeginDragLocalPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(eventData.pointerDrag.GetComponent<RectTransform>(),
            eventData.position, eventData.pressEventCamera, out BeginDragLocalPos);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        OnDragLocalPos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(eventData.pointerDrag.GetComponent<RectTransform>(),
            eventData.position, eventData.pressEventCamera, out OnDragLocalPos)) {
            Vector3 distance = new Vector3(OnDragLocalPos.x - BeginDragLocalPos.x, OnDragLocalPos.y - BeginDragLocalPos.y, 0);
            transform.localPosition += distance;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!allowExchange) {
            return;
        }
        GameObject dropGameObject = eventData.pointerCurrentRaycast.gameObject;
        if (dropGameObject != null) {
            if (dropGameObject.CompareTag("SplitScene")&& dropGameObject!=this.gameObject) {
                Vector3 tempPos = dropGameObject.transform.position;
                SplitSceneController dropSplitSceneController = dropGameObject.GetComponent<SplitSceneController>();
                Vector3 tempStaicPos = dropSplitSceneController.staticPos;

                dropGameObject.transform.position = staticPos;
                dropSplitSceneController.staticPos = staticPos;
                this.transform.position = tempStaicPos;
                this.staticPos = tempStaicPos;
            }
            else transform.position = staticPos;
        }
        else transform.position = staticPos;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData) {

    }
}
