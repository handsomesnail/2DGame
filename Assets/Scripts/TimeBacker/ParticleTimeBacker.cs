using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ParticleSystem)),DisallowMultipleComponent]
public sealed class ParticleTimeBacker : TimeBacker {

    private struct _State {
        public float ProcessTime;
    }

    private new ParticleSystem particleSystem;

    public bool playOnAwake;//接管playOnAwake选项

    private bool isPlaying = false;
    private bool isPaused = false;
    private bool isStopped = false;
    private float processTime = 0;

    protected override void Awake () {
        base.Awake();
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem.main.playOnAwake) {
            throw new Exception("可倒流粒子特效" + gameObject.name + "不能勾选PlayOnAwake");
        }
        if (playOnAwake) {
            isPlaying = true;
        }
    }

    private void Update () {
        if (!isPlaying) {
            return;
        }
        particleSystem.Simulate(processTime, true);
        processTime += Time.deltaTime;
    }

    protected override void LateUpdate() {
        _State state = new _State {
            ProcessTime = processTime
        };
        currentInverseFrames += () => {
            processTime = state.ProcessTime;
            particleSystem.Simulate(processTime, true);
        };
        base.LateUpdate();
    }

    [ContextMenu("Play")]
    public void Play() {
        if (isStopped)
            return;
        isPlaying = true;
        isPaused = false;
    }

    [ContextMenu("Pause")]
    public void Pause() {
        isPaused = true;
        isPlaying = false;
    }

    [ContextMenu("Stop")]
    public void Stop() {
        Stop(true);
    }

    public void Stop(bool isDestroy) {
        isStopped = true;
        isPlaying = false;
        if (isDestroy)
            Destroy(gameObject);
        else gameObject.SetActive(false);
    }

}
