using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiPunch : DealDamage
{
    private Animator animator;
    public Transform attackObject;
    

    public override void attack()
    {
        animator.SetTrigger("Attack");
    }

    public override void attackPass(Transform takeDamageObj)
    {
        //Fight2D.recoil(source.GetComponent<Rigidbody2D>(), takeDamageObj.transform.position, 15);
        StartCoroutine(source.GetComponent<ZombiController>().disabledControl(0.1f));
        GetComponent<ParticleSystem>().Play();
    }

    public override void Start()
    {
        animator = source.GetComponent<Animator>();
    }

    public override bool trigger()
    {
        return (transform.position - attackObject.position).magnitude < attackRadius;
    }

}