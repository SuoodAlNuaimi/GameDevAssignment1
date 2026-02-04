using UnityEngine;

public class EmemyHealthV2 : MonoBehaviour, IDamgeable
{

    public void Damage(float damageAmount)
    {

        HasTakenDamage = true;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    [SerializeField] private float maxHealth = 3f;

    private float currentHealth;

    public bool HasTakenDamage { get; set;}

    private void Start()
    {
        currentHealth = maxHealth;
    } 
}
