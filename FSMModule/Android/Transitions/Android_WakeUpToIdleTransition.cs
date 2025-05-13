using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Android_WakeUpToIdleTransition : AndroidTransition
{
    private Animator _androidAnimator;
    [SerializeField] private string _nameOfanimationForEnd;
    public override void Enable()
    {
        _androidAnimator = GetComponent<Animator>();
        StartCoroutine(WaitForAnimationCompletion());   
    }
    public void OnDisable()
    {
        _androidAnimator = null;
        StopAllCoroutines();
    }
    private IEnumerator WaitForAnimationCompletion()
    {
        // Wait for the animation to start
        while (!_androidAnimator.GetCurrentAnimatorStateInfo(0).IsName(_nameOfanimationForEnd))
            yield return null;

        // Wait for the animation to complete
        while (_androidAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            yield return null;
        
        NeedTransit = true;
    }
}
