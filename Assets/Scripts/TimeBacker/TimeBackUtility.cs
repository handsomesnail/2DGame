using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>记录每帧物理信息</summary>
public struct PhysicFrameInfo {
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale; 

    public PhysicFrameInfo(Vector3 position, Quaternion rotation, Vector3 scale) {
        this.Position = position;
        this.Rotation = rotation;
        this.Scale = scale;
    }

    public override string ToString() {
        return "Position:" + Position.ToString() + "\n" + "Rotation:" + Rotation.ToString() + "\n" + "Scale:" + Scale.ToString();
    }
}

/// <summary>记录每帧动画信息</summary>
public struct AnimatorFrameInfo {
    public int StateNameHash;
    public int LayerIndex;
    public float NormalizedTime;
    public string Motion; 
    public List<KeyValuePair<string,bool>> boolParameters;
    public List<KeyValuePair<string, int>> intParameters;
    public List<KeyValuePair<string, float>> floatParameters;

    public void AddBool(string parameterName, bool value) {
        boolParameters.Add(new KeyValuePair<string, bool>(parameterName, value));
    }

    public void AddInt(string parameterName, int value) {
        intParameters.Add(new KeyValuePair<string, int>(parameterName, value));
    }

    public void AddFloat(string parameterName, float value) {
        floatParameters.Add(new KeyValuePair<string, float>(parameterName, value));
    }

    public AnimatorFrameInfo(int stateNameHash, float normalizedTime) : this(stateNameHash, normalizedTime, -1, "default") { }

    public AnimatorFrameInfo(int stateNameHash, float normalizedTime, int layerIndex, string motion) {
        this.StateNameHash = stateNameHash;
        this.LayerIndex = layerIndex;
        this.NormalizedTime = normalizedTime;
        this.Motion = motion;
        boolParameters = new List<KeyValuePair<string, bool>>();
        intParameters = new List<KeyValuePair<string, int>>();
        floatParameters = new List<KeyValuePair<string, float>>();
    }

}


public static class TimeBackUtility {

    /// <summary>从Transform获取物理信息 </summary>
    public static PhysicFrameInfo GetPhysicInfo(this Transform transform) {
        return new PhysicFrameInfo(transform.position, transform.rotation, transform.localScale);
    }

    /// <summary>设置Transform的物理信息 </summary>
    public static void SetPhysicInfo(this Transform transform, PhysicFrameInfo physicInfo) {
        transform.position = physicInfo.Position;
        transform.rotation = physicInfo.Rotation;
        transform.localScale = physicInfo.Scale;
    }

    /// <summary>从Animator获取当前动画信息 </summary>
    public static AnimatorFrameInfo GetAnimatorFrameInfo(this Animator animator) {
        int stateHashName = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        AnimatorFrameInfo animatorFrameInfo = new AnimatorFrameInfo(stateHashName, normalizedTime);
        Array.ForEach(animator.parameters, (animatorParameter) => {
            //特殊的状态不进行倒流
            if (animatorParameter.name == "AnimatorSpeed"|| animatorParameter.name == "TimeBacking")
                return;
            switch (animatorParameter.type) {
                case AnimatorControllerParameterType.Bool:
                    animatorFrameInfo.AddBool(animatorParameter.name, animator.GetBool(animatorParameter.name));
                    break;
                case AnimatorControllerParameterType.Int:
                    animatorFrameInfo.AddInt(animatorParameter.name, animator.GetInteger(animatorParameter.name));
                    break;
                case AnimatorControllerParameterType.Float:
                    animatorFrameInfo.AddFloat(animatorParameter.name, animator.GetFloat(animatorParameter.name));
                    break;
                default:
                    break;
            }
        });
        return animatorFrameInfo;
    }

    /// <summary>设置Animator的信息 </summary>
    public static void SetAnimatorInfo(this Animator animator, AnimatorFrameInfo animatorFrameInfo) {
        animatorFrameInfo.boolParameters.ForEach((kvp) => {
            animator.SetBool(kvp.Key, kvp.Value);
        });
        animatorFrameInfo.intParameters.ForEach((kvp) => {
            animator.SetInteger(kvp.Key, kvp.Value);
        });
        animatorFrameInfo.floatParameters.ForEach((kvp) => {
            animator.SetFloat(kvp.Key, kvp.Value);
        });
    } 

}
