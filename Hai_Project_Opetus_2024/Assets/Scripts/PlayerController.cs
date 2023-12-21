using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform gunTransform;
    private Master controls;

    private Vector2 move;
    private Vector2 aim;
    private Rigidbody2D rb;
    private void Awake()
    {
        controls = new Master();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        aim = controls.Gameplay.Aim.ReadValue<Vector2>();


        if(aim.sqrMagnitude > 0.1)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPosition.z = 0; // Ensure it's on the same plane as the player

            // Calculate the direction from the gun to the mouse
            Vector2 aimDirection = (mouseWorldPosition - gunTransform.position).normalized;

            aimDirection.y = -aimDirection.y;
            //Vector2 aimDirection = new Vector2(aim.x, aim.y).normalized;
            float angle = ((float)Math.Atan2(aimDirection.x, aimDirection.y)) * Mathf.Rad2Deg;
            //angle = 180 - angle; // Adjust if the rotation is mirrored
            angle -= 90;
            gunTransform.rotation = Quaternion.Euler(0,0,angle);
        }

        //if (aimInput.sqrMagnitude > 0.1)
        //{
        //    Vector3 aimDirection = new Vector3(aimInput.x, aimInput.y, 0).normalized;
        //    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //    gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        move = controls.Gameplay.Move.ReadValue<Vector2>();
        Vector2 movement = new Vector2(move.x, move.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
