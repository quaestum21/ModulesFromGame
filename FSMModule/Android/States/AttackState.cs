public abstract class AttackState : AndroidState
{
    private Health _targetHealth;

    private void Start() => _targetHealth = Android.CarTarget.GetComponent<Health>();
    protected void CauseDamage()
    {
        float damage = float.Parse(Android.MobStat.damage);
        _targetHealth.ApplyDamage(damage);
    }
    protected void AttackEnded()
    {
    }
}
