using UnityEngine;

[RequireComponent(typeof(Android))]
public abstract class AndroidTransition : MonoBehaviour
{
    [SerializeField] private AndroidState _targetState;
    public AndroidState TargetState => _targetState;

    protected Android Android;
    public bool NeedTransit { get; protected set; }

    private void Awake() => Android = GetComponent<Android>();
    private void OnEnable()
    {
        NeedTransit = false;
        Enable();
    }
    public abstract void Enable();
}
