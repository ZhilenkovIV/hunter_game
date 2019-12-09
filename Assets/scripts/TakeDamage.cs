using System.Collections;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int healthPoints = 1;
    public Color colorImmunity;
    public float timeImmunity;
    private bool isImmunity;
    public int immunityLayer = -1;

    public delegate void DamageAction(GameObject other);
    private DamageAction damageAction;
    private DamageAction deadAction;

    void Start()
    {
        if (damageAction == null) {
            damageAction = (d) => { };
        }
        if (deadAction == null)
        {
            deadAction = (other) => Destroy(gameObject);
        }
    }

    public bool damage(int hitValue, GameObject attackObject) {

        if (attackObject.GetComponent<ObjectInfo>().type == MyObjectType.ENEMY &&
            GetComponent<ObjectInfo>().type == MyObjectType.OBJECT)
        {
            return false;
        }

        if (!isImmunity)
        {
            healthPoints -= hitValue;
            if (healthPoints > 0)
            {
                StartCoroutine(immunity(attackObject));
            }
            else {
                deadAction(gameObject);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddDamageAction(DamageAction action) {
        damageAction += action;
    }

    public void SetDeadAction(DamageAction action)
    {
        deadAction = action;
    }

    private IEnumerator immunity(GameObject other) {
        SpriteRenderer spriteRendOther = gameObject.GetComponent<SpriteRenderer>();
        spriteRendOther.color = colorImmunity;
        isImmunity = true;

        int tmpLayer = gameObject.layer;
        gameObject.layer = immunityLayer;
        damageAction(other);
        yield return new WaitForSeconds(timeImmunity);
        isImmunity = false;
        gameObject.layer = tmpLayer;
        spriteRendOther.color = Color.white;
    }


}
