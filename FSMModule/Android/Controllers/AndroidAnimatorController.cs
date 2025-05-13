using UnityEngine;

/// <summary>
///Class for changing animations depending on the state of the mob
/// </summary>
[RequireComponent(typeof(AndroidStateMachine))]
public sealed class AndroidAnimatorController : MonoBehaviour
{
    private AndroidStateMachine _robotStateMachine;
    Animator _animator;
    public void StartAnimatorController()
    {
        _robotStateMachine = GetComponent<AndroidStateMachine>();
        _animator = _robotStateMachine.Animator;
        _robotStateMachine.StateChanged += ChangeAnimation;
    }
    private void OnDestroy()
    {
        // Check that the object exists before unsubscribing
        if (_robotStateMachine != null)
            _robotStateMachine.StateChanged -= ChangeAnimation;     
    }
    private void ChangeAnimation(AndroidState state)
    {
        if (_animator == null) return;

        // checking states
        if (state is Android_WakeUpState)  
            _animator.SetTrigger("WakeUp");
        
        else if (state is Android_IdleState)
            _animator.SetTrigger("Idle");
        
        else if (state is Android_WalkState)   
            _animator.SetTrigger("Walk");
        
        else if (state is Android_RunState)      
            _animator.SetTrigger("Run");
        
        else if (state is Android_AttackState) 
            _animator.SetTrigger("Attack");
        
        else if (state is Android_JumpToTargetState)   
            _animator.SetTrigger("Jump");
        
        else if(state is Android_JumpObstacleState)
            _animator.SetTrigger("ObstacleJump");      
    }
}
