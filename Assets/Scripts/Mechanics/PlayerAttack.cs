using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount =1f;
    [SerializeField] private float timeBTWAttack = 0.15f;

    public bool ShouldBeDamaging { get; private set; } = false;

    private List<IDamgeable> iDamgeables = new List<IDamgeable>();
    private List<IDeflectable> iDeflectables = new List<IDeflectable>();


    private RaycastHit2D[] hits;

    private Animator anim; 

    private float attackTimeCounter;

    private void Start()
    {
        anim = GetComponent<Animator>();

        attackTimeCounter = timeBTWAttack;
    }

    private void Update()
    {
        if (UserInput.instance.controls.Attack.Attack.WasPressedThisFrame() && attackTimeCounter >= timeBTWAttack)
        {
            attackTimeCounter=0f;

            //Attack();
            anim.SetTrigger("ATTACK");
        }

        attackTimeCounter += Time.deltaTime;
    }

    /*
    private void Attack()
    {
        hits = Physics2D.CircleCastAll(
            attackTransform.position,
            attackRange,
            transform.right,
            0f,
            attackableLayer
        );

        for (int i = 0; i < hits.Length; i++)
        {
            IDamgeable iDamgeable = hits[i].collider.gameObject.GetComponent<IDamgeable>();

            if ( iDamgeable != null)
            {
                iDamgeable.Damage(damageAmount);
            }
        }
    }

    */



    public IEnumerator DamageWhileSlashIsActive()
    {

        ShouldBeDamaging=true;

        while(ShouldBeDamaging)
        {
            hits = Physics2D.CircleCastAll(
            attackTransform.position,
            attackRange,
            transform.right,
            0f,
            attackableLayer
            );

            for (int i = 0; i < hits.Length; i++)
            {
                IDamgeable iDamgeable = hits[i].collider.gameObject.GetComponent<IDamgeable>();

                if ( iDamgeable != null && !iDamgeable.HasTakenDamage)
                {
                    iDamgeable.Damage(damageAmount);
                    iDamgeables.Add(iDamgeable);
                }

                IDeflectable iDeflectable = hits[i].collider.gameObject.GetComponent<IDeflectable>();

                if (iDeflectable != null && !iDeflectables.Contains(iDeflectable))
                {
                    Vector2 deflectDir = (hits[i].transform.position - transform.position).normalized;
                    iDeflectable.deflect(deflectDir);

                    iDeflectables.Add(iDeflectable);
                }
            }

            yield return null;

        }

        ReturnAttackablesAndDeflectables();
        
    }


    public void ReturnAttackablesAndDeflectables()
    {
        foreach (IDamgeable thingWasDamaged in iDamgeables)
        {
            thingWasDamaged.HasTakenDamage = false;
        }

        iDamgeables.Clear();
        iDeflectables.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackTransform == null) return;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }


    public void ShouldBeDamagingToTrue()
    {
        ShouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }


}

