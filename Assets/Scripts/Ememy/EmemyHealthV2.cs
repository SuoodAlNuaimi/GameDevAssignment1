using UnityEngine;

public class EmemyHealthV2 : MonoBehaviour, IDamgeable
{

    public void Damage(float damageAmount)
    {

        HasTakenDamage = true;

        currentHealth -= damageAmount;

        SpawnDamageParticles(); 

        if (currentHealth <= 0)
        {
            Die();
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private ParticleSystem damageParticles;

    private float currentHealth;

    private ParticleSystem damageParticleInstance;

    private EmemyHealthBar healthBar;

    public bool HasTakenDamage { get; set;}

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<EmemyHealthBar>();
    } 

    private void SpawnDamageParticles()
    {
        damageParticleInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
    }
}
