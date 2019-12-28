using System.Collections;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Animator animator;
    private bool blockControlFlag = false;

    public float timeImmunity = 2f;

    private void recoil(GameObject other, Vector2 power, float timeBlock) {
        Vector2 pointFrom = other.transform.position;
        Vector2 dir = (pointFrom - rb.position);
        dir.x = (dir.x > 0) ? 1 : -1;
        dir.y = 1;
        PlayerMovement motion = GetComponent<PlayerMovement>();
        rb.velocity = dir * motion.speed * power;
        motion.BlockMovement(timeBlock);
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GetComponent<TakeDamage>().AddDamageAction((other)=> recoil(other, new Vector2(-1f, 0.5f), 0.15f));
        GetComponent<Stroke>().AddAttackAction((other)=> {
            MyObjectType typeOther = other.GetComponent<ObjectInfo>().type;
            if (typeOther == MyObjectType.ENEMY) {
                recoil(other, new Vector2(-1.2f, 0), 0.12f);
            }
        });
	}

    // Update is called once per frame
    void Update()
    {
        if(!blockControlFlag && Input.GetKeyDown(KeyCode.Z) && GetComponent<Stroke>().attack()) { 
            animator.SetTrigger("attack");
        }
    }
}
