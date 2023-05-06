using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFPMovementControllerDependencies
{
    Rigidbody Rigidbody { get; }
    Transform YAxisTransform { get; }
}

[RequireComponent(typeof(IFPMovementControllerDependencies))]
public class FPMovementController : MonoBehaviour
{
    [SerializeField] private float _movementForce = 2f;
    [SerializeField, Range(0f,1f)] private float _movementAccuracy = 0.1f;

    private IFPMovementControllerDependencies _dependencies;

    private Vector2 _inputVelocity;
    private Vector3 _targetVelocity;

    private void Awake()
    {
        _dependencies = GetComponent<IFPMovementControllerDependencies>();
    }

    private void FixedUpdate()
    {
        _targetVelocity = 
            ((_dependencies.YAxisTransform.right * _inputVelocity.x) + (_dependencies.YAxisTransform.forward * _inputVelocity.y)).normalized 
            * _movementForce;

        _dependencies.Rigidbody.velocity = new Vector3(
            Mathf.Lerp(_dependencies.Rigidbody.velocity.x, _targetVelocity.x, _movementAccuracy),
            _dependencies.Rigidbody.velocity.y,
            Mathf.Lerp(_dependencies.Rigidbody.velocity.z, _targetVelocity.z, _movementAccuracy));
    }

    public void SetInput(Vector2 input)
    {
        _inputVelocity = input;
    }
}
