using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Android))]
public class AndroidStateMachine : MonoBehaviour
{
    [SerializeField] private AndroidState _firstState;
    private AndroidState _currentState;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Android _currentRobot;

    public event UnityAction<AndroidState> StateChanged;
    public AndroidState CurrentState { get => _currentState; private set => _currentState = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public void StartMachine()
    {
        _currentRobot = GetComponent<Android>();
        CurrentState = _firstState;
        CurrentState.Enter(_rigidbody, Animator, _currentRobot); 
        gameObject.GetComponent<AndroidAnimatorController>().StartAnimatorController();
        StateChanged?.Invoke(CurrentState); // Вызов события при старте      
    }
    private void Update()
    {
        if (CurrentState == null)
            return;

        AndroidState nextState = CurrentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }
    private void Transit(AndroidState nextState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = nextState;
        StateChanged?.Invoke(CurrentState);

        if (CurrentState != null)
            CurrentState.Enter(_rigidbody, Animator, _currentRobot);
    }
}
