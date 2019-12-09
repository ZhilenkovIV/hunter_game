using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool onGround;
    public bool onlyX = false;
    public int numberLayerGround;
    public Vector2 speed;
    public LayerMask groundMask;
    private bool canChange = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround |= (groundMask & (1 << collision.gameObject.layer)) != 0;
        //onGround = !collision.otherCollider.IsTouchingLayers(groundMask);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = collision.otherCollider.IsTouchingLayers(groundMask);
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private IEnumerator BlockMovementEnum(float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        canChange = true;
    }

    public void BlockMovement(float time) {
        StartCoroutine(BlockMovementEnum(time));
    }


    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = rb.velocity.y / speed.y;
            if (moveX != 0)
            {
                sprite.flipX = moveX < 0;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && onGround)
            {
                moveY = 1;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                moveY = Mathf.Min(moveY, 0);
            }
            rb.velocity = new Vector2(moveX, moveY) * speed;
        }
    }
}
