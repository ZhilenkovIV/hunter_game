using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float hit = 1;
    public LayerMask hitLayer;
    public GameObject source;
    public float attackRadius = 2.5f;
    public float period;
    public float delay;
    protected bool canAttack = true;

    virtual public bool trigger() { return false; }

    virtual public void attack() { }

    virtual public void attackPass(Transform takeDamageObj) { }

    private IEnumerator action()
    {
        canAttack = false;
        attack();
        yield return new WaitForSeconds(delay);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, hitLayer);
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<TakeDamage>())
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

    void Update()
    {
        if (trigger() && canAttack)
        {
            StartCoroutine(action());
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

}
