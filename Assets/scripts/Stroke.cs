using System.Collections;
using UnityEngine;

public class Stroke : MonoBehaviour
{
    private bool canAttack = true;
    public float minPeriod;
    public GameObject prefabAttack;

    public Vector2 size;
    public Vector2 offset;

    private HitArea.AttackAction attackAction;


    IEnumerator startAttack(GameObject parent)
    {
        
        GameObject areaAttack = Instantiate(prefabAttack, new Vector3(), Quaternion.identity);
        areaAttack.transform.parent = parent.transform;
        areaAttack.GetComponent<HitArea>().AddAttackAction(attackAction);

        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        areaAttack.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
        areaAttack.transform.localScale = new Vector3(1, 1, 1);

        if (sprite.flipX)
        {
            areaAttack.transform.localPosition = new Vector2(-offset.x, offset.y);
        }
        else {
            areaAttack.transform.localPosition = offset;
        }

        Animator animatorAttack = areaAttack.GetComponent<Animator>();
        yield return new WaitForSeconds(animatorAttack.GetCurrentAnimatorStateInfo(0).length);
        Destroy(areaAttack);
    }

    public bool attack() {
        if (canAttack)
        {
            StartCoroutine(startAttack(gameObject));
            StartCoroutine(blockAttackIter(minPeriod));
            return true;
        }
        return false;
    }

    private IEnumerator blockAttackIter(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    public void AddAttackAction(HitArea.AttackAction action) {
        attackAction += action;
    }


    
}
