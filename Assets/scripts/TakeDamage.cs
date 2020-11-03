using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    //в качестве параметра принимается собственный игровой объект
    public Fight2D.LambdaAction dieAction;
    //в качестве параметра принимается наносящий урон объект
    public Fight2D.LambdaAction damageAction;
    public float immunityTime;
    public Color immunityColor = Color.red;
    private int unitLayer;

    private IEnumerator immunity(float time) {
        GetComponent<SpriteRenderer>().color = immunityColor;
        gameObject.layer = 13;
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.layer = unitLayer;
    }

    public float hp = 1;

    private void Start()
    {
        unitLayer = gameObject.layer;
        if (dieAction == null)
        {
            dieAction = (n) => Destroy(n);
        }
        if (damageAction == null)
        {
            damageAction = (n) => { };
        }
    }

    public void damage(DealDamage dealDamage) {
        hp -= dealDamage.hit;
        if (hp <= 0) {
            dieAction(gameObject);
        }
        damageAction(dealDamage.source);
        if (immunityTime != 0)
        {
            StartCoroutine(immunity(immunityTime));
        }
    }



}
