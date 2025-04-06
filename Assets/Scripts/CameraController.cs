using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followFactor = 1;

    private Vector3 _deltaPos;
    void Start()
    {
        _deltaPos = transform.position - _target.position;
    }


    void FixedUpdate()
    {
        Vector3 newPos = _target.position + _deltaPos;
        transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime * _followFactor);
    }
}
