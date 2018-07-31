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

    private float lastAxisY = 0;
    private float currentAxisY = 0;
    public bool isSplit = false;//当前是否是分屏状态

    [Header("触屏相关输入")]
    //public bool JoystickControlJump = false; //摇杆控制跳和蹲
    public EasyTouch easyTouch;
    public GameObject MobileInputCanvas;
    public ETCJoystick MoveJoyStick;
    public GameObject Buttons;
    public MobileButton JumpButton;
    public MobileButton CrouchButton;
    public MobileButton GravityButton;
    public MobileButton ItemButton;
    public Collider2D BackgroundRaycaster;

    [Header("键鼠相关输入")]
    public KeyCode GravityKey; 
    public KeyCode ItemKey;

    [Space(5)]
    public UnityEvent OnClickGravity;//输入重力转换操作
    public UnityEvent OnClickItem;//输入使用道具操作
    public UnityEvent OnPinchScene;//输入切到分屏操作

    public static bool deadRestart = false;

    private void Awake() {
        Instance = this;
        interactable = true;
        GravityButton.onClick.AddListener(()=>OnClickGravity.Invoke());
        ItemButton.onClick.AddListener(() => OnClickItem.Invoke());
        BackgroundRaycaster.enabled = false;
    }

    private void Start() {
//#if UNITY_STANDALONE && !_TOUCH
//        easyTouch.gameObject.SetActive(false); 
//        MobileInputCanvas.SetActive(false);
//#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
//        easyTouch.gameObject.SetActive(true);
//        MobileInputCanvas.SetActive(true);
//#endif
        OnPinchScene.AddListener(() => {
            Debug.Log("响应开始分屏");
            Interactable = false;
            BackgroundRaycaster.enabled = true;
            PlayerData.Player.GetComponent<PlayerController>().enabled = false;
            PlayerData.Player.GetComponent<Collider2D>().enabled = false;
            SplitSceneController2.splitScenes.ForEach((scene) => {
                scene.collider2D.enabled = true;
            });
            isSplit = true;
        });//分屏时屏蔽操作
    }

    private void Update() {
        if (!Interactable)
            return;

        if (Input.GetKeyDown(GravityKey))
            OnClickGravity.Invoke();
        else if (Input.GetKeyDown(ItemKey))
            OnClickItem.Invoke();

#if UNITY_STANDALONE && !_TOUCH
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
        lastAxisY = currentAxisY;
        currentAxisY = AxisY;
#endif
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
#if UNITY_EDITOR
            MoveJoyStick.enableKeySimulation = value;
#endif
        }
    }

    /// <summary>X轴输入</summary>
    public float AxisX {
        get {
#if UNITY_STANDALONE && !_TOUCH
            if (!Interactable)
                return 0;
            return Input.GetAxis("Horizontal");
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            return MoveJoyStick.axisX.axisValue;
#endif
        }
    }

    /// <summary>Y轴输入</summary>
    public float AxisY {
        get {
#if UNITY_STANDALONE && !_TOUCH
            if (!Interactable)
                return 0;   
            return Input.GetAxis("Vertical")*(-GravityManager.Instance.direction.y);
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            return MoveJoyStick.axisY.axisValue*(-GravityManager.Instance.direction.y);
#endif
        }
    }

    /// <summary>当前跳跃键是否被按下</summary>
    public bool JumpKeyDown {
        get {
#if UNITY_STANDALONE && !_TOUCH
            return Input.GetButtonDown("Jump");
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            return currentAxisY >= 0.3f & lastAxisY < 0.3f;
#endif
        }
    }

    public bool JumpKeyUp {
        get {
#if UNITY_STANDALONE && !_TOUCH
            return Input.GetButtonUp("Jump");
#elif UNITY_ANDROID || UNITY_IPHONE || _TOUCH
            return currentAxisY < 0.3f & lastAxisY >= 0.3f;
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
            return AxisY < -0.3f;
#endif
        }
    }

    public void GravityTrigger() {
        OnClickGravity.Invoke();
    }

    public void ItemTrigger() {
        OnClickItem.Invoke();
    }

    public void Restart() {
        Time.timeScale = 1;
        deadRestart = true;
        StartCoroutine(SceneManager.Instance.ConvertSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name));
    }

    public void ReturnMainMenu() {
        Time.timeScale = 1;
        StartCoroutine(SceneManager.Instance.ConvertSceneAsync("MainScene"));
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Resume() {
        Time.timeScale = 1;
    }

}
