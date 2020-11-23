using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public event System.Action dieAction;
    public event System.Action<GameObject> damageAction;

    public float immunityTime;
    public Color immunityColor = Color.red;
    private int unitLayer;
    public bool isImmunuted = false;

    private IEnumerator immunity(float time) {
        GetComponent<SpriteRenderer>().color = immunityColor;
        gameObject.layer = 13;
        isImmunuted = true;
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.layer = unitLayer;
        isImmunuted = false;
    }

    public float hp = 1;

    private void Start()
    {
        unitLayer = gameObject.layer;
    }

    public void damage(DealDamage dealDamage) {
        hp -= dealDamage.hit;
        if (hp <= 0) {
            dieAction();
        }
        if (damageAction != null)
        {
            damageAction(dealDamage.gameObject);
        }
        if (immunityTime != 0)
        {
            StartCoroutine(immunity(immunityTime));
        }
    }



}
