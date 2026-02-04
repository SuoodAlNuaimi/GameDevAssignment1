using UnityEngine;

public interface IDamgeable
{
    public void Damage(float damageAmount, Vector2 attackDirection);

    public bool HasTakenDamage {get; set;}
}
