using UnityEngine;


public class Flee : SteeringBehaviour
{
    [SerializeField]
    private float steeringPower;

    public override Vector3 Direction => CalculateSteering();

   
    private Vector3 CalculateSteering()
    {
        Vector2 originPos = transform.position;
        Vector2 targetPos = _target.position;

        // Dirección opuesta al target, normalizada
        Vector2 fleeDir = (originPos - targetPos).normalized;

        // Queremos ir a máxima velocidad
        Vector3 desiredVelocity = fleeDir * _maxSpeed;

        // Cálculo del steering
        Vector3 steer = desiredVelocity - velocity;

        return steer * steeringPower;
    }

    private void OnDrawGizmos()
    {
        if (_target == null)
            return;

        Vector3 origin = transform.position;
        Vector3 dirToTarget = origin - _target.position;
        float distance = dirToTarget.magnitude;

        // Línea azul: dirección opuesta al target, con longitud igual a la distancia
        Vector3 fleeDirScaled = dirToTarget.normalized * distance;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + fleeDirScaled);
    }

}