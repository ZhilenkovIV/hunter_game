using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float Damage;
    public float AttackRange;
    public LayerMask DamageableLayerMask;

    public System.Func<bool> AttackTrigger = ()=>true;

    public System.Action<Damageable> HitAction;
    //public 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(AttackTrigger()){
            Attack();
        }
    }

    private void Attack(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AttackRange, DamageableLayerMask);
        foreach(Collider2D coll in colliders){
            Damageable target = coll.GetComponent<Damageable>();
            if(target !=null){
                if (!target.isImmunuted && target.HealthPoints > 0) {
                    target.HealthPoints -= Damage;
                    Debug.Log("a");
                    //HitAction?.Invoke(target);
                    //target.damageAction?.Invoke(source);
                }
                //Fight2D.Action(AttackHit, target);
                //hittingTargets.Add(target);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
