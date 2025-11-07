using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Transform _target;

    [SerializeField]
    private float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find("Player").transform;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = _target.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

}

