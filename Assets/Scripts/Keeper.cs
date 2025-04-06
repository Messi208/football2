using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private float _force = 7;

    private Rigidbody _rb;
    private Animator _animator;
    private bool _haveball;
    private BallCatcher _catcher;
    private Vector3 _pos;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _catcher = GetComponentInChildren<BallCatcher>();
    }

    void Update()
    {
        _haveball = _catcher.transform.childCount > 0;
        _ball.isKinematic = _haveball;

        if (Vector3.Distance(transform.position, _ball.position) < 10 && !_haveball)
        {
            _rb.velocity = (_ball.transform.position - transform.position).normalized * _speed;
        }
        if (_haveball)
            Strike();
    }

    [ContextMenu("Strike")]
    private void Strike()
    {
        _ball.isKinematic = false;
        _haveball = false;
        _ball.AddForce((-transform.forward + transform.up) * _force, ForceMode.Impulse);
    }

}
