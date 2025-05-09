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
        // Ждем пока анимация начнется
        while (!_androidAnimator.GetCurrentAnimatorStateInfo(0).IsName(_nameOfanimationForEnd))
            yield return null;
        
        // Ждем завершения анимации
        while (_androidAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            yield return null;
        
        NeedTransit = true;
    }
}
