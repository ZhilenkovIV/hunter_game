using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float stopDistX = 0;

    private FollowBehavior followBehavior;

    
    public ICommand attackCommand;
    public ICommand bodyAttackCommand;
    public ICommand motion;


    public float attackRadius;

    public IEnumerator disableControl(float time) {
        followBehavior.enabled = false;
        yield return new WaitForSeconds(time);
        followBehavior.enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<TakeDamageModel>().damageAction +=
            (n)=> {
                Debug.Log(Mathf.Sign(n.transform.lossyScale.x));
                rb.AddForce(15 * Mathf.Sign(n.transform.lossyScale.x) * Vector2.right, ForceMode2D.Impulse);
                //Fight2D.recoil(GetComponent<Rigidbody2D>(), rb.position, 15);
                StartCoroutine(disableControl(0.1f));
            };
        GetComponent<TakeDamageModel>().dieAction += () => Destroy(gameObject);

        motion = GetComponent<MoveXCommand>();

        attackCommand = GetComponent<ZombiAttack>();

        followBehavior = GetComponent<FollowBehavior>();
    }

    private void FixedUpdate()
    {
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (followBehavior.IsInMinDistance)
        {
            attackCommand.Execute();
        }
    }


}
