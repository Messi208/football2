using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _image;

    private Camera _camera;
    [SerializeField] private bool _isVisible;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes( _camera );
        _isVisible = planes.All(plane => plane.GetDistanceToPoint(_target.position) >= 0);
        _image.SetActive(! _isVisible );
    
        if (!_isVisible)
        {
           Vector3 targetPos = _camera.WorldToScreenPoint(_target.position);
           Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
            targetPos -= screenCenter;

            Vector3 screenBounds = screenCenter *0.9f;
            targetPos = new Vector3(Mathf.Clamp(targetPos.x, -screenBounds.x, screenBounds.x),
                                    Mathf.Clamp(targetPos.y, -screenBounds.y, screenBounds.y),
                                    targetPos.z);

            _image.transform.localPosition = targetPos;

            float angle = Mathf.Atan2(targetPos.y, targetPos.x);
            _image.transform.rotation = Quaternion.Euler(0, 0, 90 + angle * Mathf.Rad2Deg);
        }
    }
}
