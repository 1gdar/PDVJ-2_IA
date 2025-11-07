using UnityEngine;


public class Seek : SteeringBehaviour 
{

    [SerializeField]
    private float steeringPower;

    public override Vector3 SteeringDir => CalculateSteering();


    private Vector3 CalculateSteering()
    {

        Vector3 desiredDirection = _target.position - transform.position;

        float distance = desiredDirection.magnitude;

        // Fuera del radio - velocidad normal
        desiredDirection = desiredDirection.normalized * _maxSpeed;
        Vector3 steer = desiredDirection - velocity;

        steer *= steeringPower;

        ///Nos va a retornar una direcccion
        return steer;

    }

   


}