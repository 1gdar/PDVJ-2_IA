using UnityEngine;


public class AgentChaseState<T> : State<T>
{
    private Transform _target;
    private float _maxSpeed;
    private float _chaseSpeed;
    private float _rotationSpeed;
    private Agent_FSM_Controller _controller;

    public AgentChaseState(T stateID,  Transform NPCAgent,Agent_FSM_Controller controller, FSM<T> fsm, Transform target, float maxSpeed , float rotSpeed)
       : base(stateID, NPCAgent, fsm)
    {
        _target = target;
        _maxSpeed = maxSpeed;
        _rotationSpeed = rotSpeed;
        _controller = controller;
    }

 

    public override void Enter()
    {
        base.Enter();
        _chaseSpeed = _maxSpeed; 
    }

    public override void Execute()
    {
        base.Execute();
        // Lógica de Chase: Moverse hacia el objetivo.
        if (_target != null)
        {
            Vector3 direction = (_target.position - _agentTransform.position).normalized;
            _agentTransform.position += direction * _chaseSpeed * Time.deltaTime;

            if (direction.sqrMagnitude > 0.01f)
            {
                // Creamos la variable que apunta en la dirección de movimiento
                Vector3 dir = new Vector3(direction.x, direction.y, 0).normalized;

                // Creamos la rotación objetivo
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, dir);

                // Rotación suave
                _agentTransform.rotation = Quaternion.RotateTowards(_agentTransform.rotation, targetRotation, _rotationSpeed);
            }
        }

     
    }

    public override void CheckConditions()
    {
        if (!_controller.CanChase())
        {
            _controller.SetIdle();
        }
        if (_controller.CanFlee())
        {
            _controller.SetFlee();
        }
    }

    public Vector3 CalculateSteering()
    {
        // Dirección opuesta al target
        Vector3 fleeDir = (_agentTransform.position - _target.position.normalized);

        // Queremos ir a máxima velocidad
        Vector3 direction = fleeDir * _maxSpeed;

        // Cálculo del steering
        Vector3 steer = direction;

        return steer;
    }
}
