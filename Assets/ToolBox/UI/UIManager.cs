using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UI_Manager : MonoBehaviour
{
    private Dictionary<string, UI_State> allStates = new Dictionary<string, UI_State>();
    private Dictionary<Type, ModalWindow> modals = new Dictionary<Type, ModalWindow>();

    public static UI_Manager Instance;


    [HideInInspector] public UI_State currentState;
    private Stack<string> statesHistory = new Stack<string>();

    private ModalWindow currentModal;

    private void Start()
    {
        Instance = this;
        if (allStates.Count < 1)
            Initialize();        
    }

    private void Initialize()
    {
        var states = transform.GetComponentsInChildren<UI_State>(true);

        foreach (var state in states)
        {
            Add(state);
            state.gameObject.SetActive(false);
            state.ConnectState(this);
        }

        currentState = states[0];
        currentState.Show();

        var modalStates = transform.GetComponentsInChildren<ModalWindow>(true);
        foreach (var m in modalStates)
        {
            modals.Add(m.GetType(), m);
            m.gameObject.SetActive(false);
            m.ConnectState(this);
        }
    }

    private void Add(UI_State state)
    {
        allStates.Add(state.name, state);
    }

    private UI_State Get(string name)
    {
        return allStates[name];
    }

    public void GoToState(UI_State state)
    {
        GoToState(state.name);
    }

    public void GoToState(string stateName, bool needReturn = true)
    {
        if (needReturn)
            statesHistory.Push(currentState.name);

        if (currentState != null)
            currentState.Hide();
        currentState = Get(stateName);
        currentState.Show();
    }
    public void GoToPreviousState()
    {
        if (statesHistory.Count == 0) return;
        var prevState = statesHistory.Pop();

        GoToState(prevState);
    }

    public T GetUIState<T>(string stateName) where T : UI_State
    {
        var state = Get(stateName);
        return (T)state;
    }

    public T GetUIState<T>() where T : UI_State
    {
        return GetUIState<T>(typeof(T).ToString());
    }


    public void OpenModalWindow(ModalWindow window)
    {
        if (modals.Count < 1) Initialize();

        if (currentModal) currentModal.Hide();
        currentModal = modals[window.GetType()];
        currentModal.Show();
    }


    public void OpenModalWindow<T>() where T : ModalWindow
    {
        var modal = GetModal<T>();
        OpenModalWindow(modal);
    }


    public T GetModal<T>() where T : ModalWindow
    {
        if (modals.Count < 1) Initialize();
        return (T)modals[typeof(T)];
    }

}


