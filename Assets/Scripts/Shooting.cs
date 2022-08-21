using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ObjectPool))]

public class Shooting : MonoBehaviour
{
    private Movement _movement;
    private InputBindings _inputBindings;
    private ObjectPool _objectPool;

    private float _lastFired;
    private bool _isShooting;
    private Quaternion _quaternionRotation;
    [SerializeField] private float fireRate = 10.00f;
    [SerializeField] private Transform bulletStartingPosition;
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        _objectPool = GetComponent<ObjectPool>();
    }
    
    private void Awake()
    {
        _inputBindings = new InputBindings();

        _inputBindings.Player.Fire.started += OnFireInput;
        _inputBindings.Player.Fire.canceled += OnFireInput;
        _inputBindings.Player.Fire.performed += OnFireInput;

        _inputBindings.Player.MousePosition.performed += ReadMouseInput;
    }

    private void OnEnable()
    {
        _inputBindings.Player.Enable();
    }

    private void OnDisable()
    {
        _inputBindings.Player.Disable();
    }
    
    private void ReadMouseInput(InputAction.CallbackContext context)
    {
        var mousePosition = context.ReadValue<Vector2>();
        var objectPosition = (Vector2) mainCamera.ScreenToWorldPoint(transform.position);
        var direction = (mousePosition - objectPosition).normalized;
        RotateAim(direction);
    }
    
    private void RotateAim(Vector2 direction)
    {  
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _quaternionRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    
    private void Shoot()
    {
        if (Time.time - _lastFired > 1 / fireRate)
        {
            _lastFired = Time.time;
            _objectPool.GetFreeElement(bulletStartingPosition.position, _quaternionRotation);
        }
    }

    private void OnFireInput(InputAction.CallbackContext ctx)
    {
        Shoot();
    }
}
