using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//使用动画回退有一定的限制条件：
//1.不能嵌套 只有一层layer 可以有BlendTree
//2.每个State的speedMultiplier绑定名为AnimatorSpeed的float类型参数
//3.每个Translation添加一个名为TimeBacking的bool参数为false的条件
//4.位移相关最好只依赖于Animator中的参数，或者将其依赖其它参数的变化回退方法另外添加
[RequireComponent(typeof(Animator)), DisallowMultipleComponent]
public sealed class AnimatorTimeBacker : MonoBehaviour, ITimeBacker {

    private LinkedList<AnimatorFrameInfo> animatorFrameInfos;//动画帧
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        animatorFrameInfos = new LinkedList<AnimatorFrameInfo>();
    }

    private  void Start() {
        TimeBackManager.Instance.AddTimeBacker(this);
    }

    private  void OnDestroy() {
        TimeBackManager.Instance.RemoveTimeBacker(this);
    }

    private void Update() {
        if (TimeBackManager.Instance.IsBacking) {
            return;
        }
        if(animatorFrameInfos.Count == TimeBackManager.MaxFrameCount) {
            animatorFrameInfos.RemoveFirst();
        }
        AnimatorFrameInfo animatorFrameInfo = animator.GetAnimatorFrameInfo();
        animatorFrameInfos.AddLast(animatorFrameInfo);
    }

    private void FixedUpdate() {
        if (TimeBackManager.Instance.IsBacking) {
            return;
        }
    }


    public void OnTimePause() {
        animator.SetFloat("AnimatorSpeed", -1.0f);
        animator.SetBool("TimeBacking", true);
        enabled = false;
    }

    public void OnTimeResume() {
        animator.SetFloat("AnimatorSpeed", 1.0f);
        animator.SetBool("TimeBacking", false);
        enabled = true;
    }

    public void B_Update() {
        AnimatorFrameInfo animatorFrameInfo = animatorFrameInfos.Last.Value;
        animatorFrameInfos.RemoveLast();
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != animatorFrameInfo.StateNameHash) {
            animator.Play(animatorFrameInfo.StateNameHash, animatorFrameInfo.LayerIndex, animatorFrameInfo.NormalizedTime);
        }
        animator.SetAnimatorInfo(animatorFrameInfo);
    }

}
