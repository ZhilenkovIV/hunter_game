using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianStroke : MonoBehaviour, ICommand
{
    public float hit = 1;
    public LayerMask hitLayer;
    public float attackRadius = 2.5f;
    public float period;
    public float delay;
    private bool canAttack = true;
    public Vector2 offset;
    public string skillName;

    private Animator animator;
    private FollowBehavior followBehavior;

    private void Start()
    {
        animator = GetComponent<Animator>();
        followBehavior = GetComponent<FollowBehavior>();
    }

    private IEnumerator action()
    {
        canAttack = false;
        IsCoolDown = true;
        float currentTime = 0;

        animator.SetTrigger("stroke");
        followBehavior.canMove = false;
        for (; currentTime < delay; currentTime += Time.deltaTime) {
            yield return null;
        }
        for (; currentTime < period; currentTime += Time.deltaTime)
        {
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
            yield return null;
        }
        followBehavior.canMove = true;
        IsCoolDown = false;
        canAttack = true;
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(Mathf.Sign(transform.localScale.x) * offset.x, offset.y, 0), attackRadius);
    }
}
