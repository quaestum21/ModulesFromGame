using UnityEngine;

public class Android_WalkToAttackTransition : AndroidTransition
{
    public override void Enable() { }
    
    private void Update()
    {
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        if (Android.PlayerOnPickup || Android.PlayerTarget == null)
            return;

        float distanceToTarget = Vector3.Distance(Android.transform.position, Android.PlayerTarget.position);

        if (distanceToTarget < 1.3f)
            NeedTransit = true;
    }
}
