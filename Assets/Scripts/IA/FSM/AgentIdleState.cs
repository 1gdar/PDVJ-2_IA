using UnityEngine;

public class AgentIdleState<T> : State<T>
{
    private Agent_FSM_Controller _controller;


    public AgentIdleState(T stateID,  Transform NPCAgent, Agent_FSM_Controller controller, FSM<T> fsm) : base(stateID, NPCAgent, fsm)
    {
        _agentTransform= NPCAgent;
        _fsm = fsm; 
        _controller = controller;
       
    }

    public override void Enter()
    {
        base.Enter();
    
    }

    public override void Execute()
    {
        base.Execute();

    }
    public override void CheckConditions()
    {
        if (_controller.CanChase())
        {
            _controller.SetChase();
        }

    }
    public override void Sleep()
    {
        base.Sleep();
    }


}
