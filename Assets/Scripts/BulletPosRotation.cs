using UnityEngine;
using UnityEngine.InputSystem;

public class BulletPosRotation : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private InputBindings _inputBindings;
    
    private void Awake()
    {
        _inputBindings = new InputBindings();
        
        _inputBindings.Player.Rotation.started += ReadMouseInput;
        _inputBindings.Player.Rotation.canceled += ReadMouseInput;
        _inputBindings.Player.Rotation.performed += ReadMouseInput;
        
        _inputBindings.Player.Rotation.started += ReadStick;
        _inputBindings.Player.Rotation.canceled += ReadStick;
        _inputBindings.Player.Rotation.performed += ReadStick;
    }

    private void ReadMouseInput(InputAction.CallbackContext context)
    {
        var mousePosition = context.ReadValue<Vector2>();
        var objectPosition = (Vector2) mainCamera.WorldToScreenPoint(transform.position);
        var direction = (mousePosition - objectPosition).normalized;
 
        RotateAim(direction);
    }
    private void ReadStick(InputAction.CallbackContext context)
    {
        var stickDirection = context.ReadValue<Vector2>().normalized;
 
        RotateAim(stickDirection);
    }
    private void RotateAim(Vector2 direction)
    {  
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
