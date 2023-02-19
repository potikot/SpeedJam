using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerControls playerControls;

    public float speed = 8f;
    private Vector2 move;
    private Vector2 jump;
    public float jumpPower = 16f;
    private Vector3 forceDirection;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            transform.position += Vector3.up * jumpPower;
        }
    }

    private void Update()
    {
        movePlayer();
    }

    public void movePlayer()
    {
        if (move.sqrMagnitude > 0.1f)
        {
            Vector3 movement = new Vector3(-move.x, 0f, -move.y);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-movement), 0.15f);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else 
            return false;
    }
}
