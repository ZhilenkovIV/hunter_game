using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canAttack = true;
    public float minPeriod;
    public GameObject prefabAttack;
    public float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator startAttack(GameObject parent)
    {
        GameObject areaAttack = Instantiate(prefabAttack, new Vector3(), Quaternion.identity);
        areaAttack.transform.parent = parent.transform;

        BoxCollider2D thisCollider = GetComponent<BoxCollider2D>();
        BoxCollider2D areaCollider = areaAttack.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(thisCollider, areaCollider);

        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        areaAttack.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
        areaAttack.transform.localScale = new Vector3(1, 1, 1);

        if (sprite.flipX)
        {
            areaAttack.transform.localPosition = new Vector3(-thisCollider.size.x / 2 - areaCollider.size.x / 2, 0, 0);
        }
        else
        {
            areaAttack.transform.localPosition = new Vector3(thisCollider.size.x / 2 + areaCollider.size.x / 2, 0, 0);
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

    // Update is called once per frame
    void Update()
    {
    }

    
}
