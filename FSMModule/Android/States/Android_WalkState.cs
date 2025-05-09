public class Android_WalkState : Android_MoveState
{
    private void Update() => MoveToTarget(Android.WalkSpeed);
}
