using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAttack : MonoBehaviour, ICommand
{
    public HitModel model;
    private Animator animator;

    public float delay;
    public float period;
    public float attackRadius;

    public bool canAttack = true;

    private IEnumerator attack() {
        float currentTime = 0;
        animator.SetTrigger("Attack");
        canAttack = false;
        for (; currentTime < delay; currentTime += Time.deltaTime) {
            yield return null;
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(model.transform.position, attackRadius, model.hitLayer);
        foreach (Collider2D c in colliders)
        {
            TakeDamageModel target = c.GetComponent<TakeDamageModel>();
            if (target != null && !target.isImmunuted)
            {
                Fight2D.Action(model, target);
            }
        }
        for (; currentTime < period; currentTime += Time.deltaTime)
        {
            yield return null;
        }
        canAttack = true;
    }

    public void Execute()
    {
        if (canAttack)
        {
            StartCoroutine(attack());
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(model.transform.position, attackRadius);
    }

}
