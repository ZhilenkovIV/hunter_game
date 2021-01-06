using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float stopDistX = 0;
    public bool canMove = true;

    
    public ICommand attackCommand;
    public ICommand bodyAttackCommand;
    public ICommand motion;


    public float attackRadius;

    public IEnumerator disableControl(float time) {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<TakeDamageModel>().damageAction +=
            ()=> {
                //Fight2D.recoil(GetComponent<Rigidbody2D>(), rb.position, 15);
                StartCoroutine(disableControl(0.1f));
            };
        GetComponent<TakeDamageModel>().dieAction += () => Destroy(gameObject);
        attackCommand = GetComponent<DealDamage>();

        GetComponent<DealDamage>().attack += ()=> {
            GetComponent<Animator>().SetTrigger("Attack");
        };

        GetComponent<DealDamage>().attackPass += (n) => {
            //Fight2D.recoil(source.GetComponent<Rigidbody2D>(), takeDamageObj.transform.position, 15);
            StartCoroutine(disableControl(0.1f));
        };

        motion = GetComponent<MoveXCommand>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") {
            attackCommand.Execute();
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }


}
