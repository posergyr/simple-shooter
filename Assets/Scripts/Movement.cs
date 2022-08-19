using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector2 _mMove;
    private Vector2 _mFire;
    private Vector2 _currentInputVector;
    private Vector2 _currentInputVelocity;
    
    [SerializeField] private float speed = 200.00f;
    [SerializeField] private float smoothInputSpeed = 0.15f;
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    { 
        _mMove = value.Get<Vector2>();
    }
    
    private void OnFire(InputValue value)
    {
        Debug.Log("Fire!");
    }

    private void FixedUpdate()
    {
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _mMove, ref _currentInputVelocity, smoothInputSpeed);
        var smoothMovement = new Vector2(_currentInputVector.x, _currentInputVector.y); 
        _rb.velocity = smoothMovement * (speed * Time.deltaTime);
    }
}
