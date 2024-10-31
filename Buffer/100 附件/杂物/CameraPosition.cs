using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _offset;
    private Transform _targetPosition;

    private void Awake()
    {
        _camera = Camera.main;
        _targetPosition = GameObject.Find("Player").transform;
        _offset = transform.position - _targetPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraPositionMovement();
    }

    private void CameraPositionMovement()
    {
        var desiredPosition = _targetPosition.position + _offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.15f);
        transform.position = smoothedPosition;
    }
}
