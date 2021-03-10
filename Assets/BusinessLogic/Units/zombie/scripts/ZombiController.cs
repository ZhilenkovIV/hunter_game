using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float stopDistX = 0;

    public ICommand attackCommand;
    public ICommand bodyAttackCommand;
    public IMotion motion;

    public float attackRadius;

    public Detector detectPlayer;
    public Detector lostZone;
    private Follower follower;
    private Patroler patroler;


    public string targetTag = "Player";
    private Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<TakeDamageModel>().damageAction +=
            (n)=> rb.AddForce(15 * Mathf.Sign(n.transform.lossyScale.x) * Vector2.right, ForceMode2D.Impulse);
        GetComponent<TakeDamageModel>().dieAction += () => Destroy(gameObject);

        motion = GetComponent<MoveXCommand>();

        attackCommand = GetComponent<ZombiAttack>();

        follower = GetComponent<Follower>();
        patroler = GetComponent<Patroler>();

        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Rigidbody2D>();
        detectPlayer.Enter += () => {
            if(!follower.enabled){
                follower.enabled = true;
                follower.SetTarget(target);
                patroler.enabled = false;
            }
        };
        lostZone.Exit += () => {
            if(follower.enabled){
                follower.ClearTarget();
                follower.enabled = false;
                patroler.point = rb.position;
                patroler.enabled = true;
                motion.Suspend(2.0f);
            }
        };
    }

    private void FixedUpdate()
    {
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        //attackCommand.Execute();
    }



}
