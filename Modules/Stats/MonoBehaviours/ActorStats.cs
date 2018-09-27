using UnityEngine;

public class ActorStats : MonoBehaviour
{
    [SerializeField]
    private IntegerStat _health;

    public int Health
    {
        get
        {
            return _health.current;
        }
    }

    public int DamageOutput
    {
        get
        {
            return 1;
        }
    }
    
    private void SetHealth(int value)
    {
        _health.current = Mathf.Clamp(_health.current + value, 0, _health.max);
    }

    public bool HealthMaxed()
    {
        return _health.current >= _health.max;
    }

    public void IncreaseHealth(int value)
    {
        SetHealth(value);
    }

    public void DescreaseHealth(int value)
    {
        SetHealth(-value);
    }
}
