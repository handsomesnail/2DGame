﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCanvasController : MonoBehaviour
{
    public Animator animator;
    public Text textMeshProUGUI;

    protected Coroutine m_DeactivationCoroutine;

    protected readonly int m_HashActivePara = Animator.StringToHash("Active");

    private void OnEnable()
    {
        ActivateCanvasWithText(textMeshProUGUI.text);
        DeactivateCanvasWithDelay(3f);
    }

    IEnumerator SetAnimatorParameterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(m_HashActivePara, false);
    }

    public void ActivateCanvasWithText(string text)
    {
        if (m_DeactivationCoroutine != null)
        {
            StopCoroutine(m_DeactivationCoroutine);
            m_DeactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        animator.SetBool(m_HashActivePara, true);
        textMeshProUGUI.text = text;
    }

    public void DeactivateCanvasWithDelay(float delay)
    {
        m_DeactivationCoroutine = StartCoroutine(SetAnimatorParameterWithDelay(delay));
    }
}
