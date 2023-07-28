public class IdleState : BaseState
{
    private RunningState _runningState;
    private FightingState _fightingState;
    private DeathState _deathState;
    private InputManager _inputManager;

    private void Start()
    {
        _runningState = GetComponent<RunningState>();
        _fightingState = GetComponent<FightingState>();
        _deathState = GetComponent<DeathState>();
        _inputManager = InputManager.Instance;        
    }

    public override void Construct()
    {
        stateName = "Idle";
    }
    
    public override void Transition()
    {
        if (_inputManager.Joystick)
        {
            playerMotor.MoveToDirection(transform.position + _inputManager.MoveVector);
            playerMotor.ChangeState(_runningState);
        }
        
        if (playerMotor.IsFighting)
        {
            playerMotor.ChangeState(_fightingState);
        }
        
        if (playerMotor.IsDead)
        {
            playerMotor.ChangeState(_deathState);
        }
    }
}