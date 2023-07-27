using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4.0f;
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Animator _animator;

    private Transform _target;		// Target to follow
    private NavMeshAgent _agent;		// Reference to our agent
    private Rigidbody _rb;

    private bool _isRunning = false;
    private bool _isFighting = false;
    private bool _isDead = false;

    private int animRunningBool;
    private int animFightingBool;
    private int animDeadBool;

    void Start () {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _movementSpeed;
        _rb = GetComponent<Rigidbody>();

        //_state = GetComponent<IdleState>();
        //_state.Construct();

        AssignAnimationID();
    }

    private void AssignAnimationID()
    {
        animFightingBool = Animator.StringToHash("isFighting");
        animRunningBool = Animator.StringToHash("isMoving");
        animDeadBool = Animator.StringToHash("isDead");
    }
    
    private void Update ()
    {
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        // Are we changing state?
        //_state.Transition();
        
        var _moveVector = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
        if (_moveVector != Vector3.zero)
        {
            MoveToDirection(transform.position + _moveVector);
        }
        else
        {
            StopMoving();
        }
        
        if (_isRunning == false) return;
        
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    _animator.SetBool(animRunningBool, false);
                    _isRunning = false;
                }
            }
        }
    }
    
    public void StopMoving()
    {
        _agent.isStopped = true;
        _animator.SetBool(animRunningBool, false);
        _isRunning = false;
    }

    public void MoveToDirection(Vector3 direction)
    {
        _agent.isStopped = false;
        _agent.SetDestination(direction);
        _animator.SetBool(animRunningBool, true);
        _isRunning = true;
    }
}
