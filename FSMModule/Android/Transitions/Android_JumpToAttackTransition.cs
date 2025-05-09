using System.Collections;
using UnityEngine;

public class Android_JumpToAttackTransition : AndroidTransition
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

        while (_androidAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            yield return null;
        
        float moveDuration = 0.2f;  
        float elapsedTime = 0f;
        Vector3 _startPosition = transform.localPosition;
        Vector3 _endPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z +1);

        while (_androidAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            elapsedTime += Time.deltaTime;  
            float t = Mathf.Clamp01(elapsedTime / moveDuration); 
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, t);  

            yield return null;
        }

        NeedTransit = true;
    }
}
