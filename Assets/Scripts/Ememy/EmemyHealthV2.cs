using UnityEngine;

public class EmemyHealthV2 : MonoBehaviour, IDamgeable
{

    public void Damage(float damageAmount)
    {
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

    private void Start()
    {
        currentHealth = maxHealth;
    } 
}
