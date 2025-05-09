using UnityEngine;

public class Android_WalkToJumpObstacleTransition : AndroidTransition
{
    public override void Enable() { }
    [Range(1,5)]
    [SerializeField] private float _radius = 1f;
    void Update() =>
        CheckObstacle();

    private void CheckObstacle()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Obstacle"))
                NeedTransit = true;
        }
    }
    private void OnValidate()
    {
        if (_radius < 0.5f) _radius = 0.5f;
    }
}
