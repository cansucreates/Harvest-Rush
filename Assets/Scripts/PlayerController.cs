using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions m_InputActions;
    private InputSystem_Actions.PlayerActions m_Player;

    [SerializeField]
    private float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_InputActions = new InputSystem_Actions(); // initializing
        m_Player = m_InputActions.Player;
    }

    void Update()
    {
        HandleMovement();
    }

    void OnEnable()
    {
        m_Player.Enable(); // Enable all actions within map.
        m_Player.Move.performed += OnMove;
        m_Player.Move.canceled += OnMove;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"OnMove: {context.ReadValue<Vector2>()}");
    }

    void OnDisable()
    {
        m_Player.Disable(); // Disable all actions within map.
        m_Player.Move.performed -= OnMove;
        m_Player.Move.canceled -= OnMove;
    }

    void HandleMovement()
    {
        Vector2 input = m_Player.Move.ReadValue<Vector2>().normalized; // read the input move value (between 1 and -1 cus its normalised)
        Vector3 movement = new Vector3(input.x, input.y, 0); // direction of the movement
        transform.position += movement * moveSpeed * Time.deltaTime; // apply movement direction in position

        // clamping the position
        float clampedX = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -4, 4);
        // apply clamped position of x and y
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
