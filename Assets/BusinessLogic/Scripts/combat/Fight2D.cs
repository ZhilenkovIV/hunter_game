using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fight2D
{
    public static void Action(HitModel source, TakeDamageModel target) {
        if (!target.isImmunuted && target.HealthPoints > 0) {
            target.HealthPoints -= source.hit;
            source.HitAction?.Invoke(target);
            target.damageAction?.Invoke(source);
        }
    }
    
}
