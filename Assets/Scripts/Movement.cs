using System;using UnityEngine;using UnityEngine.InputSystem;public class Movement : MonoBehaviour{    private Rigidbody2D _rb;    private InputBindings _inputBindings;    private Vector2 _movement;    private bool _movementPressed;    private bool _fire;        private Vector2 _currentInputVector;    private Vector2 _currentInputVelocity;    [SerializeField] private float speed = 200.00f;    [SerializeField] private float smoothInputSpeed = 0.15f;        [SerializeField] private bool isSprint;    [SerializeField] private float sprintModifier = 3.00f;    private void Awake()    {        _inputBindings = new InputBindings();        _inputBindings.Player.Movement.started += OnMovementInput;        _inputBindings.Player.Movement.canceled += OnMovementInput;        _inputBindings.Player.Movement.performed += OnMovementInput;                _inputBindings.Player.Sprint.started += OnSprintInput;        _inputBindings.Player.Sprint.canceled += OnSprintInput;    }    private void OnMovementInput(InputAction.CallbackContext ctx)    {        _movement = ctx.ReadValue<Vector2>();    }    private void OnSprintInput(InputAction.CallbackContext ctx)    {        isSprint = ctx.ReadValueAsButton();    }        private void Start()    {        _rb = GetComponent<Rigidbody2D>();    }    private void OnEnable()    {        _inputBindings.Player.Enable();    }    private void OnDisable()    {        _inputBindings.Player.Disable();    }        private void FixedUpdate()    {        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _movement, ref _currentInputVelocity, smoothInputSpeed);        var smoothMovement = new Vector2(_currentInputVector.x, _currentInputVector.y);                if (isSprint)        {            _rb.velocity = smoothMovement * (speed * sprintModifier * Time.deltaTime);        } else {            _rb.velocity = smoothMovement * (speed * Time.deltaTime);        }    }}