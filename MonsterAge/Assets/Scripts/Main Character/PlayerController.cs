using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    enum AnimationState
    {
        MainCharacterIdle,
        MainCharacterWalk,
        MainCharacterRun,
        MainCharacterJump
    }

    #region public variable
    #endregion

    #region private variable

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private GameInputManager gameInputManager;
    private InputAction _playerMove;
    private InputAction _playerJump;
    private InputAction _playerRun;

    private AnimationState _currentState;

    private Animator _anim;
    private Rigidbody _rb;

    private Vector2 _moveDirection2D;
    private Vector3 _moveDirection3D;

    [SerializeField] private bool _isRunning;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _groundCheck;

    #endregion

    #region Unity Method

    private void Awake()
    {
        gameInputManager = new GameInputManager();
    }

    private void OnEnable()
    {
        _playerMove = gameInputManager.Player.Move;

        _playerJump = gameInputManager.Player.Jump;
        _playerJump.performed += _ => _isJumping = true;
        _playerJump.canceled += _ => _isJumping = false;

        _playerRun = gameInputManager.Player.Run;
        _playerRun.performed += _ => _isRunning = true;
        _playerRun.canceled += _ => _isRunning = false;

        _playerJump.Enable();
        _playerMove.Enable();
        _playerRun.Enable();
    }

    private void OnDisable()
    {
        _playerMove.Disable();
        _playerJump.Disable();
        _playerRun.Disable();
    }

    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _anim = this.GetComponent<Animator>();

    }

    void Update()
    {
        _moveDirection2D = _playerMove.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(_moveDirection2D != Vector2.zero && !_isRunning)
        {
            Move();
        }
        else if(_moveDirection2D == Vector2.zero && _isRunning)
        {
            Run();
        }
        else if (_isJumping && _groundCheck)
        {
            _groundCheck = false;
            Jump();
        }
        else if(!_groundCheck && !_isJumping || _moveDirection2D == Vector2.zero)
        {
            ChangeAnimation(AnimationState.MainCharacterIdle);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _groundCheck = true;
        }
    }

    #endregion

    #region Custom Method

    private void Move()
    {
        _moveDirection3D = new Vector3(_moveDirection2D.x, 0, _moveDirection2D.y);
        this.transform.position += _moveDirection3D * _walkSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(_moveDirection3D), Time.deltaTime * _rotateSpeed);

        ChangeAnimation(AnimationState.MainCharacterWalk);
    }

    private void Run()
    {
        _moveDirection3D = new Vector3(_moveDirection2D.x, 0, _moveDirection2D.y);
        this.transform.position += _moveDirection3D * _runSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(_moveDirection3D), Time.deltaTime * _rotateSpeed);

        ChangeAnimation(AnimationState.MainCharacterRun);
    }

    private void Jump()
    {
        _isJumping = true;
        _rb.AddForce(this.transform.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);

        ChangeAnimation(AnimationState.MainCharacterJump);
    }

    private void ChangeAnimation(AnimationState newAnimationState)
    {
        if (_currentState == newAnimationState) return;

        _anim.Play(newAnimationState.ToString());

        _currentState = newAnimationState;
    }

    #endregion
}
