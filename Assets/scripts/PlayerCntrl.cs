using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer sprite;
    public Vector2 speed;
    public bool onGround = false;

    public Transform checkGroud;
    public float checkRadius;

    public LayerMask layerGround;
    public int jumps;

    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(checkGroud.position, checkRadius, layerGround);
        if (onGround) {
            jumps = 1;
        }
        
        float moveX = UnityEngine.Input.GetAxis("Horizontal");
        if (moveX > 0)
        {
            sprite.flipX = false;
        }
        else if(moveX < 0){
            sprite.flipX = true;
        }
        rigidbody.velocity = Vector2.right * moveX * speed.x;
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumps > 0) {
            rigidbody.velocity = Vector2.up * speed.y;
            jumps--;
        }
    }
}
