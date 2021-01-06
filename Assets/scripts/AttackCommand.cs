using UnityEngine;
using System.Collections;

public class AttackCommand : MonoBehaviour, ICommand
{
    public Transform attackPrefab;
    public float lifeTime;
    public bool isParentDepended = true;
    public string animatorTriggerName;
    private Animator animator;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private IEnumerator processAttack() {
        Transform obj;
        if (isParentDepended)
            obj = Instantiate(attackPrefab, transform);
        else
            obj = Instantiate(attackPrefab);
        yield return new WaitForSeconds(lifeTime);
        Destroy(obj);
    }

    public void Execute()
    {
        animator.SetTrigger(animatorTriggerName);
        StartCoroutine(processAttack());
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }

}
