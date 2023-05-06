using System;
using UnityEngine;

public interface IFPCameraRotationControllerDependencies
{
    public Transform YAxisTransform { get; }
    public Transform XAxisTransform { get; }
}

[RequireComponent(typeof(IFPCameraRotationControllerDependencies))]
public class FPCameraRotationController : MonoBehaviour
{
    [SerializeField] private CursorLockMode _cursorLockMode;
    [SerializeField] private float _lookSensitivity = 1f;
    [SerializeField, Range(0f, 1f)] private float _lookAccuracy = 0.5f;
    [SerializeField] private float _maxLookUpAngle = 80f;
    [SerializeField] private float _maxLookDownAngle = -80f;

    private IFPCameraRotationControllerDependencies _dependencies;

    private Vector2 _inputValue;
    private Quaternion _targetYRotation = Quaternion.identity;
    private Quaternion _targetXRotation = Quaternion.identity;

    private void Awake()
    {
        _dependencies = GetComponent<IFPCameraRotationControllerDependencies>();
        Cursor.lockState = _cursorLockMode;
    }

    private void Update()
    {
        float xRot = _inputValue.y * Time.deltaTime * _lookSensitivity;
        float yRot = _inputValue.x * Time.deltaTime * _lookSensitivity;

        _targetYRotation *= Quaternion.Euler(Vector3.up * yRot);
        _targetXRotation *= Quaternion.Euler(Vector3.left * xRot);

        _targetXRotation = ClampRotationAroundXAxis(_targetXRotation, _maxLookDownAngle, _maxLookUpAngle);

        _dependencies.YAxisTransform.localRotation = Quaternion.Lerp(_dependencies.YAxisTransform.localRotation, _targetYRotation, _lookAccuracy);
        _dependencies.XAxisTransform.localRotation = Quaternion.Lerp(_dependencies.XAxisTransform.localRotation, _targetXRotation, _lookAccuracy);
    }

    public void SetInput(Vector2 input)
    {
        _inputValue = input;
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion rotation, float minAngle, float maxAngle)
    {
        rotation.x /= rotation.w;
        rotation.y /= rotation.w;
        rotation.z /= rotation.w;
        rotation.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(rotation.x);
        angleX = Mathf.Clamp(angleX, minAngle, maxAngle);
        rotation.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return rotation;
    }
}