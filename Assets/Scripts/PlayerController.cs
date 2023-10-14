using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool movementEnabled = true;
    
    private Rigidbody rigidbody;
    
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    private float groundCheckRadius = 0.3f;
    private bool isGrounded;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce;
    private bool isRunning;
    
    private Vector3 currentDirection;

    private bool hasLevel1Key;

    [SerializeField] private GameObject jumpBlocker;
    [SerializeField] private float jumpPadForce;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementEnabled = true;
    }

    void Update()
    {
        if (movementEnabled)
        {
            currentDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            // use Physics to check for grounded (detection object under player)
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayerMask);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                // can jump
                rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            }
        }
    }
    
    void FixedUpdate()
    {
        if (movementEnabled)
        {
            isRunning = currentDirection.magnitude > 0.1f;
        
            if (isRunning)
            {
                // move position of player
                Vector3 direction = transform.forward * currentDirection.z;
                rigidbody.MovePosition(rigidbody.position + direction * (moveSpeed * Time.fixedDeltaTime));
            
                // adjust rotation of character (based on new direction)
                Quaternion rightDirection = Quaternion.Euler(0f, currentDirection.x * (turnSpeed * 100 * Time.fixedDeltaTime), 0f);
                Quaternion newRotation = Quaternion.Slerp(rigidbody.rotation, rigidbody.rotation * rightDirection, Time.fixedDeltaTime);
                rigidbody.MoveRotation(newRotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key1"))
        {
            Debug.Log("Got key!");
            hasLevel1Key = true;
            // disable key
            other.gameObject.SetActive(false);
            // disable X object blocking jump pad
            jumpBlocker.SetActive(false);
        }

        if (other.gameObject.CompareTag("JumpPad"))
        {
            // launch the player!
            Debug.Log("Up you go!");
            rigidbody.AddForce(new Vector3(0f, jumpPadForce, 0f), ForceMode.Impulse);
        }
    }

    public void StopMovement()
    {
        movementEnabled = false;
    }

    public void StartMovement()
    {
        movementEnabled = true;
    }

}
