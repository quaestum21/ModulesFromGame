public sealed class Android_IdleToWalkTransition : AndroidTransition
{
    public override void Enable() { }
    private void Update()
    {
        if(!Android.CarTarget && Android.TypeOfMove == MovementAnroidType.Walk)
            NeedTransit = true;      
    }  
}
