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


    public event System.Action attack;

    public event System.Action<Transform> attackPass;

    private IEnumerator action()
    {
        canAttack = false;
        if(attack != null)
            attack();
        yield return new WaitForSeconds(delay);
        //yield return currentTime < delay;
        Vector2 position = transform.position;
        Vector2 currentOffset = offset;
        currentOffset.x *= Mathf.Sign(transform.localScale.x);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + currentOffset, attackRadius, hitLayer);

        foreach (Collider2D hit in colliders)
        {
            TakeDamage target = hit.GetComponent<TakeDamage>();
            if (target!= null && !target.isImmunuted)
            {
                hit.GetComponent<TakeDamage>().damage(this);
                if(attackPass != null)
                    attackPass(hit.transform);
            }
        }
        yield return new WaitForSeconds(period);
        canAttack = true;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(Mathf.Sign(transform.localScale.x) * offset.x, offset.y,0), attackRadius);
    }

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
