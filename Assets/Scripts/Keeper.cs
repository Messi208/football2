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
        _pos = transform.position;
    }

    void Update()
    {
        _haveball = _catcher.transform.childCount > 0;
        _ball.isKinematic = _haveball;

        if (Vector3.Distance(_pos, _ball.position) < _radius && !_haveball)
        {
            _rb.isKinematic = false;
            _rb.linearVelocity = (_ball.transform.position - transform.position).normalized * _speed;
        }
        else
        {
            _rb.linearVelocity = (_pos - transform.position).normalized * _speed;

            if (Vector3.Distance(transform.position, _pos) < 1)
                _rb.isKinematic = true;
        }

        if (_haveball)
            StartCoroutine(Strike());
    }

    [ContextMenu("Strike")]
    private IEnumerator Strike()
    {
        _catcher.GetComponent<SphereCollider>().enabled = false;
        _ball.isKinematic = false;
        transform.localEulerAngles = Vector3.up * Random.Range(-60, 60);
        _ball.AddForce((-transform.forward + transform.up) * _force, ForceMode.Impulse);
        _ball.transform.parent = null;
        yield return new WaitForSeconds(1);
        _catcher.GetComponent <SphereCollider>().enabled = true;
    }

}
