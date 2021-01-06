using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianCharge : MonoBehaviour, ICommand
{
    private Animator animator;
    public float chargeSpeed;
    public float cooldownTime;
    private float tempSpeed;

    private bool CanAttack = true;

    private IEnumerator attackProcess()
    {
        CanAttack = false;
        GetComponent<MorticianController>().enabled = false;
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
            yield return null;
        }
        //move.speed = tempSpeed;
        GetComponent<MorticianController>().enabled = true;
        yield return new WaitForSeconds(cooldownTime);
        CanAttack = true;
    }

    public void Execute()
    {
        if (CanAttack)
        {
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
        animator = GetComponent<Animator>();
    }

}
