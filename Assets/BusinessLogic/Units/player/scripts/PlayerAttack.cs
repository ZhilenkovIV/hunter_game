using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, ICommand
{
    public Attacker attacker;

    private Animator animator;
    private Animator attackerAnimator;

    public AttackState attackState;

    void Start() {
        animator = GetComponent<Animator>();
        attackerAnimator = attacker.GetComponent<Animator>();
        ParticleSystem particles = attacker.GetComponent<ParticleSystem>();
        //model.HitAction += (t) => particles.Play();
        attacker.StartAttack += () => animator.SetTrigger("Attack");
        attacker.StartAttack += () => attackerAnimator.SetTrigger("Attack");
        attacker.AttackPassed += (t) => particles.Play();
    }

    public void Execute()
    {
        attacker.Attack();
    }

    public void Undo(){
        
    }


}
