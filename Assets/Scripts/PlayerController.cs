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
        Vector2 playerMovement = m_Player.Move.ReadValue<Vector2>(); // read the move value
        Vector3 movement = new Vector3(playerMovement.x, playerMovement.y, 0); // parse it
        transform.position += movement * moveSpeed * Time.deltaTime; // change the position
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
}
