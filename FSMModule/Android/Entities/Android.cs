using UnityEngine;

public class Android : Mob
{
    
    [SerializeField] private Renderer render;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _delayAfterAttack;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _raduisAttackForWalk; 
    [SerializeField] private float _raduisAttackForRun;
    [SerializeField] private MovementAnroidType _typeOfMove;
    [SerializeField] private bool _playerOnPickup;
    public float JumpForce { get => _jumpForce; set => _jumpForce = value; }
    public float JumpDuration { get => _jumpDuration; set => _jumpDuration = value; }
    public float DelayAfterAttack { get => _delayAfterAttack; set => _delayAfterAttack = value; }
    public float WalkSpeed { get => _walkSpeed; set => _walkSpeed = value; }
    public float RunSpeed { get => _runSpeed; set => _runSpeed = value; }
    public float RaduisAttackForWalk { get => _raduisAttackForWalk; set => _raduisAttackForWalk = value; }
    public float RaduisAttackForRun { get => _raduisAttackForRun; set => _raduisAttackForRun = value; }
    public MovementAnroidType TypeOfMove { get => _typeOfMove; set => _typeOfMove = value; }
    public bool PlayerOnPickup { get => _playerOnPickup; set => _playerOnPickup = value; }

    public void AndroidSetData(Transform target, float health, Transform carTarget = null)
    {

        PlayerTarget = target;
        MobHealth.SetHealth(health);
        if (carTarget != null)
            CarTarget = carTarget;
    }
}
public enum MovementAnroidType
{
    Walk,
    Run
}