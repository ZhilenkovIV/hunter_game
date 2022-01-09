using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, ICommand
{
    public HitModel model;
    public float period;
    public float attackRadius;

    public HashSet<Damageable> passes;

    public event System.Action NotPreparedAction;

    [SerializeField]
    private bool canAttack = true;

    private Animator animator;

    public AttackState attackState;

    private IEnumerator attack() {
        animator.SetTrigger("Attack");
        model.GetComponent<Animator>().SetTrigger("Attack");
        float currentTime = 0;
        canAttack = false;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            currentTime += Time.deltaTime;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(model.transform.position, attackRadius, model.hitLayer);
            foreach (Collider2D c in colliders)
            {
                Damageable target = c.GetComponent<Damageable>();
                if (target != null && !target.isImmunuted && !passes.Contains(target))
                {
                    Fight2D.Action(model, target);
                    passes.Add(target);
                }
            }
            yield return null;
        }
        passes.Clear();
        for (; currentTime < period; currentTime += Time.deltaTime) {
            yield return null;
        }
        canAttack = true;

    }

    void Start() {
        passes = new HashSet<Damageable>();
        animator = GetComponent<Animator>();
        ParticleSystem particles = model.GetComponent<ParticleSystem>();
        model.HitAction += (t) => particles.Play();
    }

    public void Execute()
    {
        if (canAttack)
        {
            StartCoroutine(attack());
        }
        else {
            NotPreparedAction?.Invoke();
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(model.transform.position, attackRadius);
    }

}
