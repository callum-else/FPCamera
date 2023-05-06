using UnityEngine;

public interface ICameraFOVMovementEnhancerDependencies
{
    public Camera Camera { get; }
}

[RequireComponent(typeof(ICameraFOVMovementEnhancerDependencies))]
public class CameraFOVMovementEnhancer : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float _fovSmoothing = 0.1f;
    [SerializeField, Min(0)] private float _fovIncreaseAmount = 5f;

    private ICameraFOVMovementEnhancerDependencies _dependencies;

    private float _inputValue;
    private float _fovOrigin;

    private void Awake()
    {
        _dependencies = GetComponent<ICameraFOVMovementEnhancerDependencies>();
        _fovOrigin = _dependencies.Camera.fieldOfView;
    }

    private void FixedUpdate()
    {
        _dependencies.Camera.fieldOfView = Mathf.Lerp(
               _dependencies.Camera.fieldOfView,
               _fovOrigin + (_fovIncreaseAmount * _inputValue),
               _fovSmoothing);
    }

    public void SetInput(float input)
    {
        _inputValue = Mathf.Clamp01(input);
    }
}
