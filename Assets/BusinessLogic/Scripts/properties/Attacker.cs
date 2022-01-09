using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public float Damage;
    public float AttackRange;
    public LayerMask DamageableLayerMask;

    public System.Func<bool> AttackTrigger = ()=>false;

    public event System.Action StartAttack;

    public event System.Action<Damageable> AttackPassed;

    public float TimeBtwAttack = 0;
    private float _timer;
    public System.Action Attack;

    //public 
    // Start is called before the first frame update
    void Start()
    {
        if(TimeBtwAttack == 0){
            Attack = AttackWithoutTimer;
        } else{
            Attack = AttackWithTimer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void CheckColliders(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AttackRange, DamageableLayerMask);
        if (colliders.Length != 0)
        {
            foreach (Collider2D coll in colliders)
            {
                Damageable target = coll.GetComponent<Damageable>();
                if (target != null)
                {
                    if (!target.isImmunuted && target.HealthPoints > 0)
                    {
                        target.HealthPoints -= Damage;
                        AttackPassed?.Invoke(target);
                        //HitAction?.Invoke(target);
                        //target.damageAction?.Invoke(source);
                    }
                    //Fight2D.Action(AttackHit, target);
                    //hittingTargets.Add(target);
                }
            }
        }
    }

    private void AttackWithoutTimer(){
        CheckColliders();
    }

    private void AttackWithTimer(){
        if(_timer < 0){
            if(AttackTrigger()){
                StartAttack?.Invoke();
                CheckColliders();
                _timer = TimeBtwAttack;
            }
        } else{
            _timer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
