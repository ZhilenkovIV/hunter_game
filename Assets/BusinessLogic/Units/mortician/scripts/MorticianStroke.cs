﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianStroke : MonoBehaviour, ICommand
{
    public HitModel model;
    public float attackRadius = 2.5f;
    public float period;
    public float delay;
    public bool canAttack = true;
    public Vector2 offset;
    public string skillName;

    private Animator animator;

    private FollowBehavior followBehavior;
    public List<TakeDamageModel> passes;

    public bool IsCoolDown { get; private set; }



    private IEnumerator restoration()
    {
        canAttack = false;
        IsCoolDown = true;
        for (float currentTime = 0; currentTime < period; currentTime += Time.deltaTime)
        {
            yield return null;
        }
        canAttack = true;
        IsCoolDown = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        followBehavior = GetComponent<FollowBehavior>();
        model.HitAction += (n) => n.GetComponent<Rigidbody2D>().AddForce(8 * Vector2.up, ForceMode2D.Impulse);
    }

    private IEnumerator action()
    {
        float currentTime = 0;

        animator.SetTrigger("stroke");
        followBehavior.canMove = false;
        for (; currentTime < delay; currentTime += Time.deltaTime) {
            yield return null;
        }
        Vector2 position = transform.position;
        Vector2 currentOffset = offset;
        currentOffset.x *= Mathf.Sign(transform.localScale.x);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + currentOffset, attackRadius, model.hitLayer);
        foreach (Collider2D c in colliders)
        {
            TakeDamageModel target = c.GetComponent<TakeDamageModel>();
            if (target != null && !target.isImmunuted && !passes.Contains(target))
            {
                Fight2D.Action(model, target);
                passes.Add(target);
            }
        }
        passes.Clear();
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Stroke"))
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        followBehavior.canMove = true;
        for (; currentTime < period; currentTime += Time.deltaTime)
        {
            yield return null;
        }
    }
    public void Execute()
    {
        if (canAttack)
        {
            StartCoroutine(action());
            StartCoroutine(restoration());
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
