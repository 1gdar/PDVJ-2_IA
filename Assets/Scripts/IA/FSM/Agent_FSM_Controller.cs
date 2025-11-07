using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Agent_FSM_Controller : MonoBehaviour
{
    private FSM<AgentAIStates> _fsm;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _chaseSpeed = 10f;
    [SerializeField]
    private float _fleeSpeed = 10f;
    [SerializeField]
    private float _rotSpeed = 10f;
    private SystemHealth _systemHealth;

    private InSightView _inSightView;

    [SerializeField]
    private float healthForFlee = 9f;


    private void Awake()
    {
        _systemHealth = GetComponent<SystemHealth>();
        _inSightView = GetComponent<InSightView>();
       
    }


    void Start()
    {
        InitializationFSM();
    }

    public void InitializationFSM()
    {
        ///Inicializar FSM
        _fsm = new FSM<AgentAIStates>();
        ///Instanciamos los estados
        AgentIdleState<AgentAIStates> idle = new AgentIdleState<AgentAIStates>(AgentAIStates.Idle, transform, this, _fsm);
        AgentFleeState<AgentAIStates> flee = new AgentFleeState<AgentAIStates>(AgentAIStates.Flee, transform,this, _fsm, _target, _fleeSpeed, _rotSpeed);
        AgentChaseState<AgentAIStates> chase = new AgentChaseState<AgentAIStates>(AgentAIStates.Chase, transform, this,_fsm, _target, _chaseSpeed, _rotSpeed);

        ///Creamos la transiciones
        idle.AddTransition(AgentAIStates.Flee, flee);
        idle.AddTransition(AgentAIStates.Chase, chase);

        flee.AddTransition(AgentAIStates.Idle, idle);
        flee.AddTransition(AgentAIStates.Chase, chase);

        chase.AddTransition(AgentAIStates.Idle, idle);
        chase.AddTransition(AgentAIStates.Flee, flee);

        _fsm.SetInit(idle);
    }

    void Update()
    {
        _fsm.OnUpdate();

    }

    public bool CanChase()
    {
        return _inSightView.IsInSight();
    }

    public bool CanFlee()
    {
        return _inSightView.InRange() && _systemHealth.CurrentHealth < healthForFlee;
    }


    public void SetChase()
    {
        _fsm.ChangeState(AgentAIStates.Chase);
    }

    public void SetIdle()
    {
        _fsm.ChangeState(AgentAIStates.Idle);
    }

    public void SetFlee()
    {
        _fsm.ChangeState(AgentAIStates.Flee);
    }
}
