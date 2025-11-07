using System.Runtime.InteropServices;
using UnityEngine;

public class SteeringAgentAI : MonoBehaviour
{
    private Seek _seek;
    private SteeringWithAvoidance _avoidance;

    [SerializeField]
    private float rotationSpeed = 5f;


    private void Awake()
    {
        _avoidance = GetComponent<SteeringWithAvoidance>();
    }

    private void Start()
    {

    }
    void Update()
    {

        // Inicialmente, la dirección de movimiento será hacia el target
        Vector3 moveDir = Vector3.zero;

        moveDir = _avoidance.GetDir;


        transform.position += moveDir * Time.deltaTime;


        if (moveDir.sqrMagnitude > 0.2f)
        {
            Vector3 newDir = Vector3.Slerp(transform.right, moveDir.normalized, rotationSpeed * Time.deltaTime);
            transform.right = newDir;
        }


    }

}


