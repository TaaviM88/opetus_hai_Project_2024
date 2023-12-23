using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Movement properties")]
    public float moveSpeed = 5f;

    [Header("Weapon properties")]
    public Transform gunTransform;
    public BulletData bulletData;
    public float fireRate = 0.5f; // Bullets per second
    private float nextFireTime = 0f; // When the player is allowed to fire again

    
    private Master controls;

    private Vector2 move;
    private Vector2 aim;
    private Rigidbody2D rb;

    private bool isUsingControllerOrKeyboard = false;
    private bool isUsingMouse = false;

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
        TogglePause();
        if (!GameManager.Instance.IsGameplay()) return;

        // Check for controller or keyboard input
        if (controls.Gameplay.Move.ReadValue<Vector2>().sqrMagnitude > 0.1)
        {
            isUsingControllerOrKeyboard = true;
            isUsingMouse = false;
        }

        // Check for mouse input
        if (Mouse.current.delta.ReadValue().sqrMagnitude > 0.1)
        {
            isUsingMouse = true;
            isUsingControllerOrKeyboard = false;
        }

        if (isUsingMouse)
        {
            AimWithMouse();
        }
        else if (isUsingControllerOrKeyboard)
        {
            AimWithControllerOrKeyboard();
        }

        Shoot();
    }

    private void Shoot()
    {
        //controls.Gameplay.Shoot.ReadValue<float>() > 0.1f jos haluaa että voi ampua nappipohjassa
        if (controls.Gameplay.Shoot.triggered && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            GameObject bullet = BulletPoolManager.Instance.GetBullet();
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;

            // Set the bullet's data and other properties as needed
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.bulletData = bulletData;

            Debug.Log("Fire!");
        }
    }
    private void TogglePause()
    {

        if (controls.Gameplay.Pause.triggered)
        

            if (GameManager.Instance.IsGameplay())
            {
                GameManager.Instance.ChangeState(GameState.Pause);
            }
            else
            {
                GameManager.Instance.ChangeState(GameState.Gameplay);
            }
    }
    
    private void AimWithMouse()
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
           
            //angle -= 90;
            gunTransform.rotation = Quaternion.Euler(0,0,angle);
        }

        
    }

    private void AimWithControllerOrKeyboard()
    {
        // Implement your logic for aiming with the controller or keyboard here
        // For example, you might use the right joystick's direction for aiming
        Vector2 controllerAim = controls.Gameplay.Aim.ReadValue<Vector2>();
      
        if (controllerAim.sqrMagnitude > 0.1)
        {
            float angle = Mathf.Atan2(controllerAim.y, controllerAim.x) * Mathf.Rad2Deg;
            gunTransform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameplay()) return;

        Move();
    }

    private void Move()
    {
        move = controls.Gameplay.Move.ReadValue<Vector2>();
        Vector2 movement = new Vector2(move.x, move.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable pickable = collision.GetComponent<IPickable>();
        if (pickable != null)
        {
            pickable.PickUp();
        }
    }
}
