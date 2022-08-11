using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UI_State : MonoBehaviour
{
    protected UI_Manager manager;
    protected ModalWindow[] childrenModals;
    [SerializeField] Animator buttonAnim;
    [SerializeField] Animator windowAnim;

    public virtual void ConnectState(UI_Manager uIManager)
    {
        childrenModals = GetComponentsInChildren<ModalWindow>(true);
        manager = uIManager;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        if (windowAnim)
            windowAnim.SetBool("show", true);
        if (buttonAnim)
            buttonAnim.SetBool("selected", true);
    }

    public virtual void Hide()
    {
        PlayTapSound();
        if (windowAnim)
        {
            windowAnim.SetBool("show", false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (buttonAnim)
            buttonAnim.SetBool("selected", false);
    }

    protected void PlayTapSound()
    {
    }



}

