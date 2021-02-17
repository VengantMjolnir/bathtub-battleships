using UnityEngine;
using UnityEngine.Events;
using RogueEyebrow.Variables;
using System;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable HP;
    public FloatReference StartingHP;

    public bool ResetHPOnStart;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    private void Start()
    {
        if (ResetHPOnStart)
        {
            HP.SetValue(StartingHP);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
        HandleDamage(damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageDealer damage = collision.gameObject.GetComponent<DamageDealer>();
        HandleDamage(damage);
    }

    private void HandleDamage(DamageDealer damage)
    {
        if (damage != null)
        {
            HP.ApplyChange(-damage.DamageAmount);
            DamageEvent.Invoke();
        }

        if (HP.Value <= 0.0f)
        {
            DeathEvent.Invoke();
        }
    }
}
