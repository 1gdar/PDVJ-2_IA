using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<T> 
{
    Dictionary<T, State<T>> _transitions 
        = new Dictionary<T, State<T>>();

    protected Transform _agentTransform;

    protected FSM<T> _fsm;

    public T StateID { get; private set; }


    public State(T stateID, Transform NPCAgent, FSM<T> fsm)
    {
        StateID = stateID;
        _agentTransform = NPCAgent;
        _fsm = fsm;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter State: " + this.GetType().Name);
    }
    public virtual void Execute()
    {
        CheckConditions();
        Debug.Log("Executing State: " + this.GetType().Name);
    }

    public virtual void Sleep()
    {

    }
    public virtual void CheckConditions()
    {

    }
    public void AddTransition(T input, State<T> state)
    {
        _transitions[input] = state;
    }
    public void RemoveTransition(T input)
    {
        if (_transitions.ContainsKey(input))
            _transitions.Remove(input);
    }
    public State<T> GetTransition(T input)
    {
        if (_transitions.ContainsKey(input))
        {
            return _transitions[input];
        }
        else
        {
            Debug.LogWarning($"[FSM] No existe transición hacia {input}");
            return null; 
        }

    }



}
