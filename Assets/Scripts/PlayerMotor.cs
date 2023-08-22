using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    public bool useNavMesh = true;
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

    private BaseState _state;
    private EnemiesManager _enemiesManager;
    private const string ENEMY_TAG = "Enemy";

    #region Properties

    public bool IsRunning => _isRunning;

    public bool IsFighting
    {
        get { return _isFighting;}
        set { _isFighting = value; }
    }

    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    #endregion

    void Start () {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _movementSpeed;
        _rb = GetComponent<Rigidbody>();

        _state = GetComponent<IdleState>();
        _state.Construct();

        _enemiesManager = FindObjectOfType<EnemiesManager>();

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
        //UpdateMotor();
        UpdatePlayerMotor();
    }

    private void UpdatePlayerMotor()
    {
        // Are we changing state?
        _state.Transition();
        
        // Debug.Log(_state.stateName);
        
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

        if (_enemiesManager.IsAnyEnemyAlive() && _isRunning == false)
        {
            _isFighting = true;
        }
        else
        {
            _isFighting = false;
        }
    }

    private void UpdateMotor()
    {
        var _moveVector = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
        if (useNavMesh)
        {
            // Are we changing state?
            //_state.Transition();
        
            _moveVector = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
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
        // else use rigidbody for moving
        else
        {
            var speedMultiplierInAir = 0.2f;
            if ((Mathf.Abs(_rb.velocity.x) > 7 || Mathf.Abs(_rb.velocity.z) > 7) && _moveVector != Vector3.zero)
            {
                speedMultiplierInAir = 0;
            }
            if (_moveVector == Vector3.zero)
            {
                _rb.velocity = Vector3.zero;
            }
            else
            {
                _rb.AddForce(_moveVector * (_movementSpeed * speedMultiplierInAir * Time.deltaTime), ForceMode.VelocityChange);
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
    
    public void ChangeState(BaseState state)
    {
        _state.Destruct();
        _state = state;
        _state.Construct();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(ENEMY_TAG))
        {
            // DAMAGE LOGIC HERE
        }
    }
}
