using UnityEngine;

public interface ICameraTileMovementEnhancerDependencies
{
    public Transform CameraTransform { get; }
    public Transform CameraLookAtTransform { get; }
    public Transform ZAxisTransform { get; }
}

[RequireComponent(typeof(ICameraTileMovementEnhancerDependencies))]
public class CameraTiltMovementEnhancer : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _headTiltSmoothing = 0.1f;
    [SerializeField, Range(0, 5)] private float _headTiltAmount = 1.5f;

    private ICameraTileMovementEnhancerDependencies _dependencies;
    private Vector2 _inputValue;

    private void Awake()
    {
        _dependencies = GetComponent<ICameraTileMovementEnhancerDependencies>();
    }

    private void Update()
    {
        _dependencies.ZAxisTransform.localRotation = Quaternion.Lerp(
            _dependencies.ZAxisTransform.localRotation,
            Quaternion.Euler(new Vector3(-_inputValue.y, 0, -_inputValue.x) * _headTiltAmount),
            _headTiltSmoothing);

        _dependencies.CameraTransform.LookAt(_dependencies.CameraLookAtTransform, _dependencies.ZAxisTransform.up);
    }

    public void SetInput(Vector2 input)
    {
        _inputValue = input;
    }
}
