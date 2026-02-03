using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount =1f;

    private RaycastHit2D[] hits;

    private Animator anim; 

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (UserInput.instance.controls.Attack.Attack.WasPressedThisFrame())
        {
            Attack();
            anim.SetTrigger("ATTACK");
        }
    }

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


    private void OnDrawGizmosSelected()
{
    if (attackTransform == null) return;
    Gizmos.DrawWireSphere(attackTransform.position, attackRange);
}


}

