using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region public variable
    #endregion

    #region private variable

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private GameInputManager gameInputManager;
    private InputAction _playerMove;

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
    }

    void Update()
    {
        _moveDirection2D = _playerMove.ReadValue<Vector2>();
        //Debug.Log(_moveDirection2D);
    }

    private void FixedUpdate()
    {
        if(_moveDirection2D != Vector2.zero)
        {
            Move();
        }
    }

    #endregion

    #region Custom Method

    private void Move()
    {
        _moveDirection3D = new Vector3(_moveDirection2D.x, 0, _moveDirection2D.y);
        _rb.AddForce(_walkSpeed * _moveDirection3D * Time.deltaTime);
        var playerForce = _walkSpeed * _moveDirection3D * Time.deltaTime;
        Debug.Log("walk" + playerForce.magnitude);
    }

    private void Jump()
    {

    }    

    #endregion
}
