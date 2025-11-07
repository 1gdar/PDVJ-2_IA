using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AgentFleeState<T> : State<T>
{
    private Transform _target;
    private float _maxSpeed;
    private float _fleeSpeed;
    private float _rotSpeed;
    private Agent_FSM_Controller _controller;

    public AgentFleeState(T stateID, Transform NPCAgent,Agent_FSM_Controller controller, FSM<T> fsm, Transform target, float maxSpeed, float rotSpeed)
        : base(stateID,NPCAgent, fsm)
    {
        _target = target;
        _maxSpeed = maxSpeed;
        _rotSpeed = rotSpeed;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        _fleeSpeed = _maxSpeed;
    }

    public override void Execute()
    {
        base.Execute();

        if (_target != null)
        {
            Vector3 direction = (_agentTransform.position- _target.position).normalized;
            _agentTransform.position += direction * _fleeSpeed * Time.deltaTime;

            if (direction.sqrMagnitude > 0.01f)
            {
                // Creamos la variable que apunta en la dirección de movimiento
                Vector3 dir = new Vector3(direction.x, direction.y, 0).normalized;

                // Creamos la rotación objetivo
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, dir);

                // Rotación suave
                _agentTransform.rotation = Quaternion.RotateTowards(_agentTransform.rotation, targetRotation, _rotSpeed);
            }
        }

       

    }

    override public void CheckConditions()
    {
        if (!_controller.CanFlee())
        {
            _controller.SetIdle();
        }
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
