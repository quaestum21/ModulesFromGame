public sealed class Android_AnyStateToDieTransition : AndroidTransition
{
    public override void Enable() { }
  
    private void Update()
    {
        if(Android.MobHealth.CurrentHealth <= 0)
            NeedTransit = true;
    }
}
