using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    private readonly float _groundCheckRadius = 0.3f;
    private bool _isGrounded;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce;
    private bool _isRunning;
    private bool _groundedOverride;
    private bool _movementEnabled = true;
    
    private Vector3 _currentDirection;

    private bool _hasLevel1Key;

    public int level2MemoryCount;
    
    [SerializeField] public int coins = 0;

    [SerializeField] private GameObject jumpBlocker;
    [SerializeField] private GameObject jumpBlockerLevel2;
    [SerializeField] private float jumpPadForce;

    [SerializeField] private GameObject audioManager;
    private AudioManager _audioManager;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _movementEnabled = true;
        _groundedOverride = false;
        _audioManager = audioManager.GetComponent<AudioManager>();
        level2MemoryCount = 0;
    }

    void Update()
    {
        if (_movementEnabled)
        {
            _currentDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            // use Physics to check for grounded (detection object under player)
            _isGrounded = Physics.CheckSphere(groundCheck.position, _groundCheckRadius, groundLayerMask);
            if (_isGrounded) {
                _groundedOverride = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || _groundedOverride))
            {
                // can jump
                if (_groundedOverride) {
                    //_rigidbody.AddForce(new Vector3(0f, jumpForce * 2, 0f), ForceMode.Impulse);
                    Vector3 currVel = _rigidbody.velocity;
                    _rigidbody.velocity = new Vector3(0f,0f,0f);
                    _rigidbody.AddForce(new Vector3(currVel.x, 8, currVel.z), ForceMode.VelocityChange);
                   
                } else {
                    _rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                }
                _isGrounded = false;
                _groundedOverride = false;
            }
        }
    }
    
    void FixedUpdate()
    {
        _animator.SetBool("isRunning", _isRunning);
        if (_movementEnabled)
        {
            _isRunning = _currentDirection.magnitude > 0.1f;
        
            if (_isRunning)
            {
                // move position of player
                Vector3 direction = transform.forward * _currentDirection.z;
                _rigidbody.MovePosition(_rigidbody.position + direction * (moveSpeed * Time.fixedDeltaTime));
            
                // adjust rotation of character (based on new direction)
                Quaternion rightDirection = Quaternion.Euler(0f, _currentDirection.x * (turnSpeed * 100 * Time.fixedDeltaTime), 0f);
                Quaternion newRotation = Quaternion.Slerp(_rigidbody.rotation, _rigidbody.rotation * rightDirection, Time.fixedDeltaTime);
                _rigidbody.MoveRotation(newRotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region Level1
        if (other.gameObject.CompareTag("Key1"))
        {
            Debug.Log("Got key!");
            _audioManager.PlayLevel1KeyPickup();
            _hasLevel1Key = true;
            // disable key
            other.gameObject.SetActive(false);
            // disable X object blocking jump pad
            jumpBlocker.SetActive(false);
        }
        #endregion

        #region Level2
        if (other.gameObject.CompareTag("Memory"))
        {
            Debug.Log("Collected a memory");
            // disable memory upon collection
            other.gameObject.SetActive(false);
            // update memory count
            level2MemoryCount++;
            // after collecting all memories, enable jump pad
            if (level2MemoryCount == 5)
            {
                jumpBlockerLevel2.SetActive(false);
            }
        }
        #endregion

        #region Level4
        if (other.gameObject.CompareTag("JumpRefresher"))
        {
            Debug.Log("touched refresher");
            _audioManager.PlayLevel1KeyPickup();
            _groundedOverride = true;
            StartCoroutine(wait(other));
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("touched coin");
            _audioManager.PlayLevel1KeyPickup();
            other.gameObject.SetActive(false);
            coins++;
        }
        #endregion        

        if (other.gameObject.CompareTag("JumpPad"))
        {
            // launch the player!
            _audioManager.PlayBouncePad();
            Debug.Log("Up you go!");
            _rigidbody.AddForce(new Vector3(0f, jumpPadForce, 0f), ForceMode.Impulse);
        }
    }

    // Functions accessible to other scripts for the purpose of suspending player movement
    //  (useful when the player is in dialogue)
    public void StopMovement()
    {
        _movementEnabled = false;
    }

    public void StartMovement()
    {
        _movementEnabled = true;
    }

    IEnumerator wait(Collider other)
    {
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        other.gameObject.SetActive(true);
    }


}
