using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _run;
    [SerializeField] private float _gravity;
    [SerializeField] private float _rotSpeed;

    private CharacterController _controller;
    private Vector3 _motion;
    private Animator _animator;    

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();        
    }

    void FixedUpdate()
    {
        Move();
        Gravity();
        Rotate();
    }

    private void Move()
    {
        _motion.z = -Input.GetAxis("Horizontal");
        _motion.x = Input.GetAxis("Vertical");

        _motion = Vector3.ClampMagnitude(_motion, 1);
        _controller.Move(_motion * Time.deltaTime * _run);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            _animator.SetBool("Run", true);
        }
        else
            _animator.SetBool("Run", false);
    }

    private void Gravity()
    {
        _motion.y = _controller.isGrounded ? 0 : _motion.y - _gravity * Time.deltaTime;
    }

    private void Rotate()
    {
        _motion = transform.InverseTransformDirection(_motion);
        float turn = Mathf.Atan2(_motion.x, _motion.z);
        transform.Rotate(0, turn * _rotSpeed * Time.deltaTime, 0);
    }

}
