using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Flee : SteeringBehaviour
{
  
    public override Vector3 SteeringDir => CalculateSteering();

   
    private Vector3 CalculateSteering()
    {
        // Dirección opuesta al target
        Vector3 fleeDir = (transform.position - _target.position.normalized);

        // Queremos ir a máxima velocidad
        Vector3 desiredVelocity = fleeDir * _maxSpeed;

        // Cálculo del steering
        Vector3 steer = desiredVelocity - velocity;

        return steer;
    }



}