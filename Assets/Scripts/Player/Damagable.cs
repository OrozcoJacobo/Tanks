using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    //This is a property that allows us to implement get and set
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            OnHealthChange?.Invoke((float)Health / _maxHealth);
        }
    }


    public UnityEvent onDead;
    public UnityEvent<float> OnHealthChange;
    public UnityEvent onHit, onHeal;

    private void Start()
    {
        _health = _maxHealth;
    }

    internal void Hit(int damagePoints)
    {
        _health -= damagePoints;
        if(Health <= 0)
        {
            onDead?.Invoke();
        }
        else
        {
            onHit?.Invoke();
        }
    }

    public void Heal(int healthBoost)
    {
        _health += healthBoost;
        _health = Mathf.Clamp(Health, 0, _maxHealth);
        onHeal?.Invoke();
    }
 
}
