using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private Camera camera;
    
   
    [Header("Movement")]
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
   
    [Header("Rotation")]
    [SerializeField] public float minYaw = -360;
    [SerializeField] public float maxYaw = 360;
    [SerializeField] public float minPitch = -60;
    [SerializeField] public float maxPitch = 60;
    [SerializeField] public float lookSensitivity = 1;
    
    private bool _grounded;
    private Vector3 _velocity;
    private float _pitch = 0;
    private CharacterController _controller;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _controller = gameObject.GetComponent<CharacterController>();
        moveInput.action.Enable();
    }
    

    void Update()
    {
        Move();
    }

    public void Move()
    {
        var input = moveInput.action.ReadValue<Vector2>();
        Vector3 move = Vector3.zero;
        move += transform.forward * input.y;
        move += transform.right * input.x;
        
        _grounded = _controller.isGrounded;
        if (_grounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
       
        _controller.Move(move * Time.deltaTime * speed);

      

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _velocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        _velocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
       
        _pitch += -Input.GetAxis("Mouse Y") * lookSensitivity;
        _pitch = ClampAngle(_pitch, minPitch, maxPitch);
        camera.transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSensitivity, 0);
    }
    
    protected float ClampAngle(float angle) {
        return ClampAngle(angle, 0, 360);
    }

    protected float ClampAngle(float angle, float min, float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }


}

