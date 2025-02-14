using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public static GameInput instance { get; private set; }
    private InputSystem_Actions inputSystem;
    public EventHandler onPlayerAttack;

    private void Awake() {
        instance = this;
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
        inputSystem.Player.Attack.started += OnPlayerAttack;
    }

    private void OnDestroy() {
        inputSystem.Player.Attack.started -= OnPlayerAttack;
    }

    private void OnPlayerAttack(InputAction.CallbackContext context) {
        onPlayerAttack.Invoke(this, EventArgs.Empty);
    }

    public bool IsMoving() {
        return inputSystem.Player.Move.IsPressed();
    }

    public bool IsSprinting() {
        return inputSystem.Player.Sprint.IsPressed();
    }

    public Vector2 GetPlayerVector() {
        return inputSystem.Player.Move.ReadValue<Vector2>();
    }

    public void GetMoveSide() {
        inputSystem.Player.Move.IsPressed();
    }

    public Vector3 GetMousePosition() {
        return Mouse.current.position.ReadValue();
    }
}