using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Android_JumpObstacleToWalkTransition : AndroidTransition
{
    private Animator animator;
    public override void Enable() => animator = GetComponent<Animator>();
    private void Update()
    {
        if (animator != null)
        {
            // Проверяем, завершена ли анимация прыжка
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ObstacleJump") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                NeedTransit = true;
        }
    }
}
