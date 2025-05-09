using UnityEngine;

[RequireComponent(typeof(Android))]
public abstract class AndroidState : MonoBehaviour
{
    [SerializeField] private AndroidTransition[] _transitions;
     private Android _android;

    public Android Android { get => _android; set => _android = value; }

    public void Enter(Rigidbody rigidbody, Animator animator, Android robot)
    {
        if(!enabled)
        {
            Android = robot;
            enabled = true;
            foreach (var transition in _transitions)
                transition.enabled = true;
        }
    }
    public void Exit()
    {
        if (enabled)
        {
            foreach(var transition in _transitions)
                transition.enabled = false;
            
            enabled = false;
        }
    }
    public AndroidState GetNextState()
    {
        foreach(var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
            
        }
        return null;
    }
    private void Start() => _android = GetComponent<Android>();
}
