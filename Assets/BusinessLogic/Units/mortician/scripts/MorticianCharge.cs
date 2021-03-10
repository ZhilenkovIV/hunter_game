using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianCharge : MonoBehaviour, ICommand
{
    public HitModel model;
    private Animator animator;
    public float chargeSpeed;
    public float cooldownTime;

    private bool CanAttack = true;

    public List<TakeDamageModel> passes;
    public float attackRadius;

    private bool firstCharge = true;



    private IEnumerator restoration()
    {
        CanAttack = false;
        for (float currentTime = 0; currentTime < cooldownTime; currentTime += Time.deltaTime)
        {
            yield return null;
        }
        CanAttack = true;
    }



    private IEnumerator attackProcess()
    {
        GetComponent<FollowBehavior>().canMove = false;
        animator.SetTrigger("charge");
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("PrepareCharge")) { 
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("PrepareCharge"))
        {
            yield return null;
        }
        MoveXCommand move = GetComponent<MoveXCommand>();

        //tempSpeed = move.speed;
        move.speed = Mathf.Sign(transform.localScale.x) * chargeSpeed;

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
        {
            GetComponent<MoveXCommand>().Execute();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, model.hitLayer);
            foreach (Collider2D c in colliders)
            {
                TakeDamageModel target = c.GetComponent<TakeDamageModel>();
                if (target != null && !target.isImmunuted && !passes.Contains(target))
                {
                    Fight2D.Action(model, target);
                    passes.Add(target);
                }
            }
            yield return null;
        }
        passes.Clear();
        move.speed = 0;
        move.Execute();
        GetComponent<FollowBehavior>().canMove = true;
    }

    public void Execute()
    {
        if (firstCharge) {
            StartCoroutine(restoration());
            firstCharge = false;
            return;
        }
        if (CanAttack)
        {
            StartCoroutine(restoration());
            StartCoroutine(attackProcess());
        }  
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        model.HitAction += (n) => {
            Rigidbody2D targetRb = n.GetComponent<Rigidbody2D>();
            if (targetRb.velocity.y == 0) {
                targetRb.AddForce(12 * Vector2.up, ForceMode2D.Impulse);
            }
        };
        animator = GetComponent<Animator>();
        passes = new List<TakeDamageModel>();
    }

}
