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

    UnityAction action;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Enemy")) {
            rebound(col.rigidbody.position, new Vector2(1, 0.5f));
            sprite.color = Color.blue;
            gameObject.layer = 13;
            Invoke("clearImmunity", timeImmunity);
        }
    }

    public void blockControl(float time) {
    }

    delegate IEnumerator blockFlag1();

    public void rebound(Vector2 pointFrom, Vector2 power) {
        Vector2 dir = (pointFrom - rb.position).normalized;
        dir.x = dir.x * -1;
        dir.y = 1;
        rb.velocity = dir * speed * power;
        blockControlFlag = true;
        Invoke("clearBlockControl", 0.2f);
    }

    void OnCollisionExit2D(Collision2D other)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        action = clearBlockControl;
    }

    void clearBlockControl()
    {
        blockControlFlag = false;
    }

    void clearImmunity()
    {
        gameObject.layer = 9;
        sprite.color = Color.white;
    }

    void setCanGroundUpdate()
    {
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
                canGroundUpdate = false;
                Invoke("setCanGroundUpdate", 0.3f);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(moveX * speed.x, Mathf.Min(rb.velocity.y, 0));
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (GetComponent<Attack>().attack())
                {
                    animator.SetTrigger("attack");
                }
            }
        }

    }
}
