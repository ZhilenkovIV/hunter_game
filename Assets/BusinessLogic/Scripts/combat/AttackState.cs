using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseUnitState
{
    public AttackState nextAttack;
    public AttackState endState;
    public System.Func<bool> NextStateTrigger;
    float timeWait;

    public Transform AttackZone;
    public float AttackRadius;
    public HitModel AttackHit;

    private float currentTime;

    public event System.Action StartAction;
    public event System.Action EndAction;

    private HashSet<Damageable> hittingTargets;
    public AttackState(AttackStateInfo info) : base(info)
    {
        hittingTargets = new HashSet<Damageable>();
    }

    public override void Entry()
    {
        currentTime = 0;
        hittingTargets.Clear();
        StartAction?.Invoke();
    }

    public override void Exit()
    {
        EndAction?.Invoke();
        if(currentTime < timeWait){
            _switcher.SetState(nextAttack);
        } else{
            _switcher.SetState(endState);
        }
    }

    public override void LogicUpdate()
    {
        if(currentTime < timeWait){
            if (NextStateTrigger()){
                Exit();
                return;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(AttackZone.position, AttackRadius, AttackHit.hitLayer);
            foreach(Collider2D coll in colliders){
                Damageable target = coll.GetComponent<Damageable>();
                if(target !=null && !hittingTargets.Contains(target)){
                    Fight2D.Action(AttackHit, target);
                    hittingTargets.Add(target);
                }
            }
            currentTime += Time.deltaTime;
        } else{
            Exit();
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
}
