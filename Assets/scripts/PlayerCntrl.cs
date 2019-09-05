using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int jumps;
    private Animator animator;
    private float moveX = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(checkGroud.position, checkRadius, layerGround);
        animator.SetBool("onGround", onGround);
        if (onGround) {
            jumps = maxJumps;
        }

        moveX = Input.GetAxis("Horizontal");
        if (moveX > 0)
        {
            sprite.flipX = false;
        }
        else if(moveX < 0){
            sprite.flipX = true;
        }

        //rigidbody.velocity.Set(moveX * speed.x, rigidbody.velocity.y);
        rb.velocity = new Vector2(moveX * speed.x, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumps > 0) {
            rb.velocity = Vector2.up * speed.y;
            onGround = false;
            jumps--;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            rb.velocity = new Vector2(moveX * speed.x, Mathf.Min(rb.velocity.y, 0));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("attack");
            if (sprite.flipX)
            {
                areaAttack.localPosition = new Vector3(-Mathf.Abs(areaAttack.localPosition.x), areaAttack.localPosition.y, areaAttack.localPosition.z);
            }
            else {
                areaAttack.localPosition = new Vector3( Mathf.Abs(areaAttack.localPosition.x), areaAttack.localPosition.y, areaAttack.localPosition.z);
            }
            areaAttack.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
            areaAttack.GetComponent<Animator>().SetTrigger("attack");
        }
    }
}
