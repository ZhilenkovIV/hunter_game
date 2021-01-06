using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour, ICommand
{
    public float hit = 1;
    public LayerMask hitLayer;
    public float attackRadius = 2.5f;
    public float period;
    public float delay;
    private bool canAttack = true;
    public Vector2 offset;
    public string skillName;


    public event System.Action attack;

    public event System.Action<Transform> attackPass;

    private IEnumerator action()
    {
        canAttack = false;
        IsCoolDown = true;
        if(attack != null)
            attack();
        yield return new WaitForSeconds(delay);
        Vector2 position = transform.position;
        Vector2 currentOffset = offset;
        currentOffset.x *= Mathf.Sign(transform.localScale.x);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + currentOffset, attackRadius, hitLayer);
        foreach (Collider2D c in colliders)
        {
            TakeDamageModel target = c.GetComponent<TakeDamageModel>();
            if (target!= null && !target.isImmunuted)
            {
                target.HealthPoints -= hit;
                if(attackPass != null)
                    attackPass(c.transform);
            }
        }
        yield return new WaitForSeconds(period);
        IsCoolDown = false;
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(Mathf.Sign(transform.localScale.x) * offset.x, offset.y,0), attackRadius);
    }

    public bool IsCoolDown { get; private set; }

    public void Execute()
    {
        if (canAttack)
        {
            StartCoroutine(action());
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
