using System;
using System.Linq;
using UnityEngine;

public class FSM<T>
{
    State<T> _current;

    public State<T> Current { get => _current; set => _current = value; }

    public FSM()
    {
       
    }
    public FSM(State<T> initState)
    {
        SetInit(initState);
    }
    public State<T> GetCurrentState()
    {
        return _current;
    }
    public void SetInit(State<T> init)
    {
        if (init == null)
        {
            Debug.LogError("[FSM] Estado inicial es NULL. No se puede iniciar la FSM.");
            return;
        }

        _current = init;
        Debug.Log("[FSM] Estado inicial establecido: " + _current.GetType().Name);
        _current.Enter();
    }
    public void OnUpdate()
    {
        if (_current == null)
        {
            Debug.LogError("[FSM] _current es NULL en OnUpdate. Verifica que SetInit() haya sido llamado.");
            return;
        }

        _current.Execute();
    }
    public void ChangeState(T input)
    {
        State<T> newState = _current.GetTransition(input);
        if (newState == null)
        {
            Debug.LogWarning("No se encontró una transición válida para el estado " + _current);
            return;  // No cambiar de estado si no hay transición
        }

        _current.Sleep();
        _current = newState;
        _current.Enter();
    }
}

