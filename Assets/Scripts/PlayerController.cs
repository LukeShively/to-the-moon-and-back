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
    [SerializeField] private bool _movementEnabled = true;
    
    private Vector3 _currentDirection;

    [SerializeField] private bool _hasLevel1Key;

    public int level2MemoryCount;
    
    [SerializeField] public int coins = 0;

    [SerializeField] private GameObject jumpBlocker;
    [SerializeField] private GameObject jumpBlockerLevel2;
    [SerializeField] private GameObject jumpBlockerLevel4; 
    [SerializeField] private float jumpPadForce;

    [SerializeField] private GameObject audioManager;
    private AudioManager _audioManager;

    [SerializeField] private GameObject level1TitleCard;
    [SerializeField] private GameObject level2TitleCard;
    [SerializeField] private GameObject level3TitleCard;
    [SerializeField] private GameObject level4TitleCard;

    [SerializeField] private GameObject endGameManager;
    private EndGameManager _endGameManager;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject level4Help;
    [SerializeField] private GameObject level3CheatPosition;
    private bool _isOnLevel3Ground;
    [SerializeField] private GameObject level1HelpHUD;
    [SerializeField] private GameObject level2HelpHUD;

    private float airTime;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _movementEnabled = true;
        _groundedOverride = false;
        _audioManager = audioManager.GetComponent<AudioManager>();
        level2MemoryCount = 0;
        level1TitleCard.SetActive(true);
        level2TitleCard.SetActive(false);
        level3TitleCard.SetActive(false);
        level4TitleCard.SetActive(false);
        level4Help.SetActive(false);
        _endGameManager = endGameManager.GetComponent<EndGameManager>();
        // set player to initial position in level 1 upon game start
        transform.position = spawnPoint.transform.position;
    }

    void Update()
    {
        if (_movementEnabled)
        {
            _rigidbody.isKinematic = false; // reset kinematic status (so forces can be applied)
            _currentDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            // use Physics to check for grounded (detection object under player)
            _isGrounded = Physics.CheckSphere(groundCheck.position, _groundCheckRadius, groundLayerMask);
            if (_isGrounded)
            {
                airTime = 0f;
                _groundedOverride = false;
            }
            else
            {
                // track total time in air
                airTime += Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || _groundedOverride))
            {
                // can jump
                if (_groundedOverride) {
                    //_rigidbody.AddForce(new Vector3(0f, jumpForce * 2, 0f), ForceMode.Impulse);
                    Vector3 currVel = _rigidbody.velocity;
                    _rigidbody.velocity = new Vector3(0f,0f,0f);
                    Debug.Log("You shouldn't see me!");
                    _rigidbody.AddForce(new Vector3(currVel.x, 8, currVel.z), ForceMode.VelocityChange);
                } else {
                    _rigidbody.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                }
                _isGrounded = false;
                _groundedOverride = false;
            }
        }
        else
        {
            // force remove velocity changes (jumping)
            _rigidbody.isKinematic = true;
            _isRunning = false;
        }

        if (_hasLevel1Key)
        {
            // disable X object blocking jump pad
            jumpBlocker.SetActive(false);
        }
        
        // cheats
        if (Input.GetKeyUp(KeyCode.G))
        {
            // enable "god mode" - disable all jump blockers
            jumpBlocker.SetActive(false);
            jumpBlockerLevel2.SetActive(false);
            jumpBlockerLevel4.SetActive(false);
            if (_isOnLevel3Ground)
            {
                transform.position = level3CheatPosition.transform.position;
            }
        }
    }
    
    void FixedUpdate()
    {
        _animator.SetBool("isRunning", _isRunning);
        _animator.SetBool("isJumping", !_isGrounded);
        if (airTime >= 0.1f)
        {
            // transition to the air born animation state if been in the air for 1.5 seconds or longer
            _animator.SetBool("isAirborn", true);
        }
        else
        {
            _animator.SetBool("isAirborn", false);
        }
        if (_movementEnabled)
        {
            _rigidbody.isKinematic = false;
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
        else
        {
            _rigidbody.isKinematic = true; // prevents AddForce from being applied (prevents jumping)
            _isRunning = false;
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
            level1HelpHUD.SetActive(false);
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
            _audioManager.PlayMemory();
            // disable memory upon collection
            other.gameObject.SetActive(false);
            // update memory count
            level2MemoryCount++;
            // after collecting all memories, enable jump pad
            if (level2MemoryCount == 5)
            {
                level2HelpHUD.SetActive(false);
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
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(new Vector3(0f, jumpPadForce, 0f), ForceMode.Impulse);
            if (coins != 0)
            {
                // meaning that the jump pad taken was from level 4 (final level)
                _endGameManager.EndTheGame();
            }
        }

        if (other.gameObject.CompareTag("SafetyNet"))
        {
            // reset the player to spawn position
            transform.position = spawnPoint.transform.position;
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
    
    // collision check to enable level's title card
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Level2Ground"))
        {
            level2TitleCard.SetActive(true);
        }
        
        if (other.gameObject.CompareTag("Level3Ground"))
        {
            _isOnLevel3Ground = true;
            level3TitleCard.SetActive(true);
        }
        
        if (other.gameObject.CompareTag("Level4Ground"))
        {
            level4Help.SetActive(true);
            level4TitleCard.SetActive(true);
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Level3Ground"))
        {
            _isOnLevel3Ground = false;
        }
    }
}
