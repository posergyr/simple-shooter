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
    [SerializeField] private float fireRate = 1.00f;
    [SerializeField] private Transform bulletStartingTransform;
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
        var objPosition = (Vector2) bulletStartingTransform.position;
        var mousePosition = (Vector2) mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        var direction = (mousePosition - objPosition).normalized;
        RotateAim(direction);
    }
    
    private void RotateAim(Vector2 direction)
    {  
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _quaternionRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90.00f));
    }
    
    private void Update()
    {
        bulletStartingTransform.rotation = _quaternionRotation;
        
        if (_isShooting)
        {
            if (Time.time - _lastFired > 1 / fireRate)
            {
                _lastFired = Time.time;
                _objectPool.GetFreeElement(bulletStartingTransform.position, _quaternionRotation);
            }
        }
    }

    private void OnFireInput(InputAction.CallbackContext ctx)
    {
        _isShooting = ctx.ReadValueAsButton();
    }
}
