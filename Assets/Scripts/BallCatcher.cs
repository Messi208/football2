using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCatcher : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _slider;
    [SerializeField] private bool _isKeeper;

    private Rigidbody _ball;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();

    }

    private void Update()
    {
        if (_isKeeper)
            return;
        if (_ball != null)
        {
            if (Input.GetMouseButton(0))
            {
                //_forceSlider.value += 0.005f;
                _image.fillAmount += 0.0008f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine(Strike());
            }
        }

        _slider.SetActive(_image.fillAmount > 0);
    }

    private IEnumerator Strike()
    {
        _animator.SetTrigger("Strike");
        yield return new WaitForSeconds(0.35f);
        _ball.AddForce((transform.forward + transform.up) * _force * _image.fillAmount, ForceMode.Impulse);
        _image.fillAmount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.transform.parent = transform;
            _ball = other.GetComponent<Rigidbody>();
            _ball.transform.position = transform.position;
            _ball.linearVelocity = Vector3.zero;
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Ball") && !Input.GetMouseButton(0))
    //        other.transform.position = transform.position;
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.transform.parent = null;
            _ball = null;
        }

    }

}
