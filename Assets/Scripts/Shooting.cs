using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private Movement _movement;
    private InputBindings _inputBindings;
    
    private bool _isShooting;
    [SerializeField] private Transform bulletStartingPosition;
    
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
        if (_isShooting)
        {
            var bulletObject = ObjectPool.Instance.GetPooledObject();

            if (bulletObject != null)
            {
                bulletObject.transform.position = bulletStartingPosition.position;
                bulletObject.SetActive(true);
            }
        }
    }

    private void OnFireInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fire!");
        _isShooting = ctx.ReadValueAsButton();
    }
}
