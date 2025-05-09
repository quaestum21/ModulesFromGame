using UnityEngine;

public class Android_AttackToWalkTransition : AndroidTransition
{
    public override void Enable() { }
    void Update()
    {
        if (Android.PlayerOnPickup)
            return;

        if (Android.PlayerTarget == null) return;

        float distanceToTarget = Vector3.Distance(Android.transform.position, Android.PlayerTarget.position);

        if (distanceToTarget > 1.8f)
            NeedTransit = true;
    }
}
