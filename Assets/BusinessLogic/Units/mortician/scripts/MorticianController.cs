using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianController : MonoBehaviour
{

    private MorticianStroke stroke;
    private ICommand charge;

    private UnitStateMachine stateMachine;

    private Transform player;

    private IdleState idle;


    // Start is called before the first frame update
    void Start()
    {
        stroke = GetComponent<MorticianStroke>();
        charge = GetComponent<MorticianCharge>();
        stateMachine = GetComponent<UnitStateMachine>();

        Follower follower = new Follower(new StateInfo(transform, stateMachine));
        player = GameObject.FindGameObjectWithTag("Player").transform;
        follower.SetTarget(player.GetComponent<Rigidbody2D>());
        follower.maxSpeed = 4f;
        stateMachine.Initialize(follower);

        idle = new IdleState(new StateInfo(transform, stateMachine));

        stroke.beginAttack += () => stateMachine.SetState(idle);
        stroke.endAttack += () => stateMachine.SetState(follower);

    }

    void Update()
    {
        Vector2 distance = player.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position;
        if (Mathf.Abs(distance.x) < Mathf.Abs(stroke.offset.x)) {
            stroke.Execute();
        }
    }


}
