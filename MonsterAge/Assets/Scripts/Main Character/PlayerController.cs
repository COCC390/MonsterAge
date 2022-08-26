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
        MainCharacterRun
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

    private AnimationState _currentState;

    private Animator _anim;
    private Rigidbody _rb;

    private Vector2 _moveDirection2D;
    private Vector3 _moveDirection3D;

    #endregion

    #region Unity Method

    private void Awake()
    {
        gameInputManager = new GameInputManager();
    }

    private void OnEnable()
    {
        _playerMove = gameInputManager.Player.Move;
        _playerMove.Enable();
    }

    private void OnDisable()
    {
        _playerMove.Disable();
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
        if(_moveDirection2D != Vector2.zero)
        {
            Move();
        }
        else
        {
            ChangeAnimation(AnimationState.MainCharacterIdle);
        }
    }

    #endregion

    #region Custom Method

    private void Move()
    {
        _moveDirection3D = new Vector3(_moveDirection2D.x, 0, _moveDirection2D.y);
        //_rb.AddForce(_walkSpeed * _moveDirection3D * Time.deltaTime, ForceMode.Impulse);
        this.transform.position += _moveDirection3D * _walkSpeed * Time.deltaTime;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(_moveDirection3D), Time.deltaTime * _rotateSpeed);

        ChangeAnimation(AnimationState.MainCharacterWalk);
    }

    private void FaceForward()
    {

    }

    private void Jump()
    {

    }    

    private void ChangeAnimation(AnimationState newAnimationState)
    {
        if (_currentState == newAnimationState) return;

        _anim.Play(newAnimationState.ToString());

        _currentState = newAnimationState;
    }

    #endregion
}
