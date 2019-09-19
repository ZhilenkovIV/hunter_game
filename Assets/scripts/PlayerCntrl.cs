using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCntrl : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public Vector2 speed;
    private bool onGround = false;

    public Transform checkGroud;
    public Transform areaAttack;
    public float checkRadius;

    public LayerMask layerGround;
    public int maxJumps = 1;
    public int jumps;
    private Animator animator;
    private float moveX = 0;
    private bool blockControl = false;
    private bool canGroundUpdate = true;

    public bool isCollision = false;

    UnityAction action;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 10) {
            Vector3 dir = (col.rigidbody.position - rb.position).normalized;
            dir = dir * -1;
            rb.velocity = dir * speed;
            blockControl = true;
            Invoke("clearBlockControl", 0.3f);
        }
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
        areaAttack.gameObject.SetActive(false);
        action = clearBlockControl;
    }

    void clearBlockControl()
    {
        blockControl = false;
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


        if (!blockControl)
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

            if (Input.GetKeyDown(KeyCode.Z) )
            {
                //blockControl = true;
                //Invoke("clearBlockControl", 0.3f);

                areaAttack.gameObject.SetActive(true);
                animator.SetTrigger("attack");
                if (sprite.flipX)
                {
                    BoxCollider2D playerBox = GetComponent<BoxCollider2D>();
                    BoxCollider2D areaBox = areaAttack.GetComponent<BoxCollider2D>();
                    areaAttack.localPosition = new Vector3(-Mathf.Abs(areaAttack.localPosition.x), areaAttack.localPosition.y, areaAttack.localPosition.z);
                }
                else
                {
                    BoxCollider2D playerBox = GetComponent<BoxCollider2D>();
                    BoxCollider2D areaBox = areaAttack.GetComponent<BoxCollider2D>();
                    areaAttack.localPosition = new Vector3(Mathf.Abs(areaAttack.localPosition.x), areaAttack.localPosition.y, areaAttack.localPosition.z);
                }
                
                areaAttack.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
                areaAttack.GetComponent<Animator>().SetTrigger("attack");
            }
        }

    }
}
