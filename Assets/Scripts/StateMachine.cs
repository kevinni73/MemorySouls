using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    struct StateInfo
    {
        public int state;
        public Func<int> updateFunc;
        public Func<IEnumerator> coroutineFunc;
        public Action beginFunc;
        public Action endFunc;
    };

    private List<StateInfo> _states;
    [SerializeField] private int _state = 0;
    public int State
    {
        get => _state;
        set
        {
            TransitionState(value, true);
        }
    }

    private IEnumerator _coroutine;

    public void Init(int size)
    {
        _states = new List<StateInfo>(new StateInfo[size]);
    }

    public void AddState(int state, Func<int> updateFunc, Func<IEnumerator> coroutineFunc, Action beginFunc, Action endFunc)
    {
        StateInfo info = new StateInfo { state = state, updateFunc = updateFunc, coroutineFunc = coroutineFunc, beginFunc = beginFunc, endFunc = endFunc };
        _states[(int)state] = info;
    }

    private StateInfo currentState()
    {
        return _states[(int)_state];
    }

    private void Start()
    {
        if (currentState().beginFunc != null)
        {
            currentState().beginFunc();
        }
    }

    private void TransitionState(int state, bool force = false)
    {
        if (!force && _state == state)
        {
            return;
        }

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (currentState().endFunc != null)
        {
            currentState().endFunc();
        }
        
        _state = state;

        if (currentState().beginFunc != null)
        {
            currentState().beginFunc();
        }

        if (currentState().coroutineFunc != null)
        {
            _coroutine = currentState().coroutineFunc();
            StartCoroutine(_coroutine);
        }
    }

    private void Update()
    {
        if (currentState().updateFunc == null)
        {
            return;
        }

        int nextState = currentState().updateFunc();
        TransitionState(nextState);
    }
}
