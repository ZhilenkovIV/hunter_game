using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    private Rigidbody2D rb;

    public ICommand attackCommand;
    public IMotion motion;

    public float BaseSpeed;

    public float attackRadius;

    public Detector detectPlayer;
    public Detector lostZone;
    public Detector attackZone;

    public string targetTag = "Player";
    private Rigidbody2D target;

    private UnitStateMachine stateMachine;

    
    public Patroler patroler2;


    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<TakeDamageModel>().damageAction +=
            (n)=> rb.AddForce(15 * Mathf.Sign(n.transform.lossyScale.x) * Vector2.right, ForceMode2D.Impulse);
        GetComponent<TakeDamageModel>().dieAction += () => Destroy(gameObject);

        motion = GetComponent<MoveXCommand>();

        attackCommand = GetComponent<ZombiAttack>();


        stateMachine = GetComponent<UnitStateMachine>();

        Patroler patroler = new Patroler(transform, stateMachine, transform.position)
        {
            speed = BaseSpeed * 0.7f,
            time = 3f
        };
        Follower follower = new Follower(transform, stateMachine)
        {
            maxSpeed = BaseSpeed
        };
        MoveToPoint motionToPoint = new MoveToPoint(transform, stateMachine, patroler.point, patroler)
        {
            speed = new Vector2(BaseSpeed * 0.8f, 0)
        };

        //stateMachine.SetState(patroler);
        stateMachine.Initialize(follower);


        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Rigidbody2D>();
        follower.SetTarget(target);


        detectPlayer.Enter += () => stateMachine.SetState(follower);

        attackZone.Enter += () => stateMachine.SetState(new IdleState(transform, stateMachine));
        attackZone.Stay += () => attackCommand.Execute();
        attackZone.Exit += () => stateMachine.SetState(follower);

        //lostZone.Exit += () =>
        //{
        //    if (stateMachine.currentState == follower)
        //    {
        //        stateMachine.SetState(new WaitState(transform, stateMachine, 1, motionToPoint));
        //    }
        //};
    }


    public void FixedUpdate()
    {
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }



}
