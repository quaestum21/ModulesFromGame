using UnityEngine;

public class Android_RunToJumpTransition : AndroidTransition
{
    [Range(1,10)]
    [SerializeField] private float _jumpDistanceThreshold = 5f; // Distance to the car at which you can jump

    private CarAttachPoints _attachPoints;

    private void Update()
    {
        if (!Android.PlayerOnPickup)
            return;
        if(_attachPoints == null)
            return;
        if (CanJump())
        {
            if (HasFreeAttachPoint()) {
                _attachPoints.SetMobOnAttachPoint(Android);
                NeedTransit = true;
            }
        }
    }
    private bool HasFreeAttachPoint()
    {
        if (_attachPoints.HasFreePoint())
        {
            int indexOfFreePoint = -1;
            indexOfFreePoint = _attachPoints.GetNumberOfFreeAttachPoint();
            if (indexOfFreePoint == -1)
                return false;
            return true;
        }
        return false;
    }
    private bool CanJump()
    {
        if (Android.CarTarget == null) return false;

        float distanceToTarget = Vector3.Distance(Android.transform.position, Android.CarTarget.position);
        
        if (distanceToTarget > _jumpDistanceThreshold)
            return false;  
        return true;
    }
    public override void Enable() => _attachPoints = Android.CarTarget.gameObject.GetComponent<CarAttachPoints>();
    private void OnValidate()
    {
        if(_jumpDistanceThreshold < 0) _jumpDistanceThreshold = 0;
    }
}