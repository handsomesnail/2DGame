using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>接管所有输入</summary>
public sealed class InputManager : MonoBehaviour {

    public static InputManager Instance {
        private set; get;
    }

    private bool interactable;

    [Header("触屏相关输入")]
    public bool JoystickControlJump = false; //摇杆控制跳和蹲
    public EasyTouch easyTouch;
    public GameObject MobileInputCanvas;
    public ETCJoystick MoveJoyStick;
    public MobileButton JumpButton;
    public MobileButton CrouchButton;
    public MobileButton GravityButton;
    public MobileButton ItemButton;

    [Header("键鼠相关输入")]
    public KeyCode GravityKey; 
    public KeyCode ItemKey;

    [Space(5)]
    public UnityEvent OnClickGravity;//输入重力转换操作
    public UnityEvent OnClickItem;//输入使用道具操作
    public UnityEvent OnPinchScene;//输入切到分屏操作

    private void Awake() {
        Instance = this;
        interactable = true;
        GravityButton.onClick.AddListener(()=>OnClickGravity.Invoke());
        ItemButton.onClick.AddListener(() => OnClickItem.Invoke());
    }

    private void Start() {
#if UNITY_STANDALONE && !_TOUCH
        easyTouch.gameObject.SetActive(false);
        MobileInputCanvas.SetActive(false);
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
        easyTouch.gameObject.SetActive(true);
        MobileInputCanvas.SetActive(true);
#endif
        OnPinchScene.AddListener(() => {
            Debug.Log("响应开始分屏");
            Interactable = false;
        });//分屏时屏蔽操作
    }

    private void Update() {
        if (!Interactable)
            return;

        if (Input.GetKeyDown(GravityKey))
            OnClickGravity.Invoke();
        else if (Input.GetKeyDown(ItemKey))
            OnClickItem.Invoke();

    }

    /// <summary>是否接收输入</summary>
    public bool Interactable {
        get {
            return interactable;
        }
        set {
            interactable = value;
            MoveJoyStick.activated = value;
            MobileInputCanvas.SetActive(value);
            easyTouch.gameObject.SetActive(value);
        }
    }

    /// <summary>X轴输入</summary>
    public float AxisX {
        get {
            if (!Interactable)
                return 0;
#if UNITY_STANDALONE && !_TOUCH
            return Input.GetAxis("Horizontal");
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            return MoveJoyStick.axisX.axisValue;
#endif
        }
    }

    /// <summary>Y轴输入</summary>
    public float AxisY {
        get {
            if (!Interactable)
                return 0;
#if UNITY_STANDALONE && !_TOUCH
            return Input.GetAxis("Vertical");
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            return MoveJoyStick.axisY.axisValue;
#endif
        }
    }

    /// <summary>当前跳跃键是否被按下</summary>
    public bool JumpKeyDown {
        get {
            if (!Interactable)
                return false;
#if UNITY_STANDALONE && !_TOUCH
            return Input.GetButtonUp("Jump");
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            if (JoystickControlJump)
                return AxisY > 0.3f;
            return JumpButton.Pressed;
#endif
        }
    }

    /// <summary>当前蹲键是否被按下</summary>
    public bool CrouchKeyDown {
        get {
            if (!Interactable)
                return false;
#if UNITY_STANDALONE && !_TOUCH
            return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            if (JoystickControlJump)
                return AxisY < -0.3f;
            return CrouchButton.Pressed;
#endif
        }
    }

}
