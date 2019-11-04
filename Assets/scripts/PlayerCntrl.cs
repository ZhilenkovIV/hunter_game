using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCntrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public Vector2 speed;
    private bool onGround;

    public Transform checkGroud;
    public float checkRadius;

    public LayerMask layerGround;
    public int maxJumps = 1;
    public int jumps;
    private Animator animator;
    private float moveX;
    private bool blockControlFlag = false;
    private bool canGroundUpdate = true;

    public float timeImmunity = 2f;

    private void recoil(GameObject other, Vector2 power) {
        Vector2 pointFrom = other.transform.position;
        Vector2 dir = (pointFrom - rb.position);
        dir.x = (dir.x > 0) ? 1 : -1;
        dir.y = 1;
        rb.velocity = dir * speed * power;
        StartCoroutine(blockControl(0.2f));
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GetComponent<TakeDamage>().AddDamageAction((other)=> recoil(other, new Vector2(-1, 0.5f)));
        GetComponent<Stroke>().AddAttackAction((other)=> {
            MyObjectType typeOther = other.GetComponent<TakeDamage>().type;
            if (typeOther == MyObjectType.ENEMY) {
                recoil(other, new Vector2(-0.7f, 0));
            }
        });
    }

    void clearBlockControl()
    {
        blockControlFlag = false;
    }

    private IEnumerator blockControl(float time) {
        blockControlFlag = true;
        yield return new WaitForSeconds(time);
        blockControlFlag = false;
    }
    private IEnumerator blockChangeCanJump(float time)
    {
        canGroundUpdate = false;
        yield return new WaitForSeconds(time);
        canGroundUpdate = true;
    }


    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(checkGroud.position, checkRadius, layerGround);
        animator.SetBool("onGround", onGround);
        if (canGroundUpdate && onGround)
        {
            jumps = maxJumps;
        }


        if (!blockControlFlag)
        {
            moveX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveX * speed.x, rb.velocity.y);
            if (moveX > 0)
            {
                sprite.flipX = false;
            }
            else if (moveX < 0)
            {
                sprite.flipX = true;
            }

            //rigidbody.velocity.Set(moveX * speed.x, rigidbody.velocity.y);

            if (Input.GetKeyDown(KeyCode.UpArrow) && jumps > 0)
            {
                rb.velocity = Vector2.up * speed.y;
                onGround = false;
                jumps--;
                StartCoroutine(blockChangeCanJump(0.3f));
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(moveX * speed.x, Mathf.Min(rb.velocity.y, 0));
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (GetComponent<Stroke>().attack())
                {
                    animator.SetTrigger("attack");
                }
            }
        }

    }
}
