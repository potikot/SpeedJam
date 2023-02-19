using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    private Vector2 move;
    private Vector2 jump;
    public float jumpPower = 16f;


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<Vector2>();
       
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
}
