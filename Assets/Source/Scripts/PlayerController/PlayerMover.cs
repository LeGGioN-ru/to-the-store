using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    private PlayerController _playerController;
    private Vector2 _moveDirection;
    private Vector2 _lookDelta;

    Vector2 rotation = Vector2.zero;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerController = new PlayerController();
    }

    private void OnEnable()
    {
        _playerController.Enable();
    }

    private void OnDisable()
    {
        _playerController.Disable();
    }

    private void Update()
    {
        _moveDirection = _playerController.Player.Move.ReadValue<Vector2>();
        _lookDelta = _playerController.Player.Look.ReadValue<Vector2>();

        Move(_moveDirection);
        Look(_lookDelta);
    }

    private void Move(Vector2 moveDirection)
    {
        float scaledSpeed = _speed * Time.deltaTime;
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(moveDirection.x, 0, moveDirection.y);
        transform.position += move * scaledSpeed;
    }

    private void Look(Vector2 lookDelta)
    {
        rotation.x += lookDelta.x * _speedRotation * Time.deltaTime;
        rotation.y += lookDelta.y * _speedRotation * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -90, 90);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
}
