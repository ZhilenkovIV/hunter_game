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
    public float checkRadius;

    public LayerMask layerGround;
    public int maxJumps = 1;
    public int jumps;
    private Animator animator;
    private float moveX = 0;
    private bool blockControlFlag = false;
    private bool canGroundUpdate = true;
    private bool isImmunity = false;

    public bool isCollision = false;
    public GameObject prefabAttack;
    private float timeImmunity = 2f;

    UnityAction action;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag.Equals("Enemy")) {
            rebound(col.rigidbody.position, new Vector2(1, 0.5f));
            isImmunity = true;
            sprite.color = Color.blue;
            gameObject.layer = 13;
            Invoke("clearImmunity", timeImmunity);
        }
    }

    public void blockControl(float time) {
    }

    public void rebound(Vector2 pointFrom, Vector2 power) {
        Vector2 dir = (pointFrom - rb.position).normalized;
        dir.x = dir.x * -1;
        dir.y = 1;
        rb.velocity = dir * speed * power;
        blockControlFlag = true;
        Invoke("clearBlockControl", 0.2f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        /*Debug.Log("trigger");
        if (isImmunity && col.tag.Equals("Enemy")) {
            Debug.Log("ignore");
            Physics2D.IgnoreCollision(col, GetComponent<BoxCollider2D>());
        }*/
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
        isImmunity = false;
        gameObject.layer = 9;
        sprite.color = Color.white;
    }

    void setCanGroundUpdate()
    {
        canGroundUpdate = true;
    }

    IEnumerator startAttack() {
        GameObject areaAttack = Instantiate(prefabAttack, new Vector3(), Quaternion.identity);
        areaAttack.transform.parent = transform;

        Collider2D thisCollider = GetComponent<BoxCollider2D>();
        Collider2D areaCollider = areaAttack.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(thisCollider, areaCollider);

        areaAttack.GetComponent<SpriteRenderer>().flipX = sprite.flipX;
        Animator animatorAttack = areaAttack.GetComponent<Animator>();
        BoxCollider2D areaBox = areaAttack.GetComponent<BoxCollider2D>();
        //areaAttack.GetComponent<SpriteRenderer>().size = areaBox.size;
        areaAttack.transform.localScale = Vector3.one;
        

        if (sprite.flipX)
        {
            BoxCollider2D playerBox = GetComponent<BoxCollider2D>();
            areaAttack.transform.localPosition = new Vector3(-playerBox.size.x / 2 - areaBox.size.x / 2, 0, 0);
        }
        else
        {
            BoxCollider2D playerBox = GetComponent<BoxCollider2D>();
            areaAttack.transform.localPosition = new Vector3( playerBox.size.x / 2 + areaBox.size.x / 2, 0, 0);
        }
        
        yield return new WaitForSeconds(animatorAttack.GetCurrentAnimatorStateInfo(0).length);
        Destroy(areaAttack);
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
                //blockControl = true;
                //Invoke("clearBlockControl", 0.3f);

                animator.SetTrigger("attack");
                StartCoroutine(startAttack());
                
            }
        }

    }
}
