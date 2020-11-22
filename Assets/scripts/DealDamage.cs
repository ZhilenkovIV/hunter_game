using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour, ICommand
{
    public float hit = 1;
    public LayerMask hitLayer;
    public GameObject source;
    public float attackRadius = 2.5f;
    public float period;
    public float delay;
    private bool canAttack = true;
    public bool isActive = true;


    virtual public bool trigger() { return false; }

    virtual public void attack() { }

    virtual public void attackPass(Transform takeDamageObj) { }

    private IEnumerator action()
    {
        canAttack = false;
        attack();
        yield return new WaitForSeconds(delay);
        //yield return currentTime < delay;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, hitLayer);
        foreach (Collider2D hit in colliders)
        {
            TakeDamage target = hit.GetComponent<TakeDamage>();
            if (target!= null && !target.isImmunuted)
            {
                hit.GetComponent<TakeDamage>().damage(this);
                attackPass(hit.transform);
            }
        }
        yield return new WaitForSeconds(period);
        canAttack = true;
    }

    // Start is called before the first frame update
    virtual public void Start() { }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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
