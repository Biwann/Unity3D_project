using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    [SerializeField] float _sensitivity = 0.2f;
    [SerializeField] GroundGenerator _groundGenerator;

    float _lastMouseX;
    float _rotationY = 0;

    float _lastMouseY;
    float _rotationX = 0;

    float _cameraScale;
    float _scaleTarget;
    float _maxScale;

    private void Start()
    {
        if (_groundGenerator != null)
        {
            var size = _groundGenerator.GetGroundSize();

            float max = (float)(size.x > size.y ? size.x : size.y);
            float scale = max / 10f;

            _cameraScale = scale;
            _scaleTarget = scale;
            _maxScale = 2f * scale;

            UpdateScale();
        }
    }
    private void Update()
    {
        RotateCamera();
        UpdateScale();
    } 

    void RotateCamera()
    {
        var mouseX = Input.mousePosition.x;
        var mouseY = Input.mousePosition.y;
        if (Input.GetMouseButtonDown(1))
        {
            _lastMouseX = mouseX;
            _lastMouseY = mouseY;
        }

        if (Input.GetMouseButton(1))
        {

            float offsetX = mouseX - _lastMouseX;
            float offsetY = -(mouseY - _lastMouseY);

            _lastMouseX = mouseX;
            _lastMouseY = mouseY;

            _rotationY += offsetX * _sensitivity;
            _rotationX += offsetY * _sensitivity;

            _rotationX = Mathf.Clamp(_rotationX, -30f, 45f);

            transform.eulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }
    }

    void UpdateScale()
    {
        _scaleTarget -= Input.GetAxis("Mouse ScrollWheel");
        _scaleTarget = Mathf.Clamp(_scaleTarget, 0.1f, _maxScale);
        _cameraScale = Mathf.MoveTowards(_cameraScale, _scaleTarget, Time.deltaTime);
        transform.localScale = new Vector3(0, _cameraScale, _cameraScale);
    }
}
