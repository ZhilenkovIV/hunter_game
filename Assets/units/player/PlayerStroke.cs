using System.Collections;
using UnityEngine;

public class PlayerStroke : DealDamage
{
    private Animator animator;
    private Animator playerAnimator;

    public override void attack()
    {
        animator.SetTrigger("Attack");
        playerAnimator.SetTrigger("Attack");
        
    }

    public override void Start()
    {
        animator = GetComponent<Animator>();
        playerAnimator = source.GetComponent<Animator>();
    }

    private IEnumerator blockControl(float deltaTime) {
        playerAnimator.GetComponent<PlayerController>().CanInputHandle = false;
        yield return new WaitForSeconds(deltaTime);
        playerAnimator.GetComponent<PlayerController>().CanInputHandle = true;
    }

    public override void attackPass(Transform takeDamageObj)
    {
        
        if (GetComponentInParent<PlayerController>().CanInputHandle)
        {
            Fight2D.recoil(source.GetComponent<Rigidbody2D>(), takeDamageObj.position, 20);
            StartCoroutine(blockControl(0.1f));
            GetComponent<ParticleSystem>().Play();
        }
    }

}
