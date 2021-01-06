using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleAttack : MonoBehaviour, ICommand
{
    public float hit = 1;
    public LayerMask hitLayer;
    public float attackRadius = 2.5f;
    public float period;
    private bool canAttack = true;
    public Vector2 offset;

    private Animator animator;

    public bool IsCoolDown { get; private set; }

    void Start() {
        animator = GetComponent<Animator>();
    }

    private IEnumerator attackProcess()
    {
        canAttack = false;
        IsCoolDown = true;

        Vector2 position = transform.position;
        Vector2 currentOffset = offset;
        currentOffset.x *= Mathf.Sign(transform.localScale.x);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + currentOffset, attackRadius, hitLayer);
        foreach (Collider2D c in colliders)
        {
            TakeDamageModel target = c.GetComponent<TakeDamageModel>();
            if (target != null && !target.isImmunuted)
            {
                target.HealthPoints -= hit;
            }
        }
        yield return new WaitForSeconds(period);
        IsCoolDown = false;
        canAttack = true;
    }

    public void Execute() {
        if (canAttack)
        {
            StartCoroutine(GetComponent<PlayerController>().disableInput(0.2f));
            animator.SetTrigger("Attack");
            StartCoroutine(attackProcess());
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
