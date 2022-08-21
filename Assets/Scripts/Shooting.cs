using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private Movement _movement;
    private InputBindings _inputBindings;

    private bool _isShooting;
    [SerializeField] private Transform bulletStartingPosition;
    private Vector3 _offsetPosition;
    
    [SerializeField] private float fireRate = 10.00f; 
    private float _lastFired;

    private void Awake()
    {
        bulletStartingPosition = GetComponent<Transform>();
        _inputBindings = new InputBindings();
        
        _inputBindings.Player.Fire.started += OnFireInput;
        _inputBindings.Player.Fire.canceled += OnFireInput;
        _inputBindings.Player.Fire.performed += OnFireInput;
    }

    private void OnEnable()
    {
        _inputBindings.Player.Enable();
    }

    private void OnDisable()
    {
        _inputBindings.Player.Disable();
    }
    
    private void Update()
    {
        _offsetPosition = bulletStartingPosition.position;
        _offsetPosition.y += 0.75f;
        
        if (_isShooting)
        {
            if (Time.time - _lastFired > 1 / fireRate)
            {
                _lastFired = Time.time;
                
                var bulletObject = ObjectPool.Instance.GetPooledObject();

                if (bulletObject != null)
                {
                    bulletObject.transform.position = _offsetPosition;
                    bulletObject.SetActive(true);
                }
            }
        }
    }

    private void OnFireInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fire!");
        _isShooting = ctx.ReadValueAsButton();
    }
}
