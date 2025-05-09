using UnityEngine;

/// <summary>
/// Класс для смены анимаций в зависимости от состояния моба
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
        // Проверяем, что объект существует перед отпиской
        if (_robotStateMachine != null)
            _robotStateMachine.StateChanged -= ChangeAnimation;     
    }
    private void ChangeAnimation(AndroidState state)
    {
        // Проверка на null аниматора
        if (_animator == null) return;

        // Логика анимаций
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
