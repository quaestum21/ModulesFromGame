public class Android_IdleToRunTransition : AndroidTransition
{
    public override void Enable() { }

    private void Update()
    {
        if (Android.CarTarget != null && Android.TypeOfMove == MovementAnroidType.Run)
            NeedTransit = true;
    }
}
