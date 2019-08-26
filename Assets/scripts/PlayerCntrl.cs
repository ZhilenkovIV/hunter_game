﻿using System.Collections;
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
    public int maxJumps = 1;
    private int jumps;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        
        float moveX = UnityEngine.Input.GetAxis("Horizontal");
        if (moveX > 0)
        {
            sprite.flipX = false;
        }
        else if(moveX < 0){
            sprite.flipX = true;
        }

        //rigidbody.velocity.Set(moveX * speed.x, rigidbody.velocity.y);
        rigidbody.velocity = new Vector2(moveX * speed.x, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumps > 0) {
            rigidbody.velocity = Vector2.up * speed.y;
            animator.Play("jump");
            jumps--;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            rigidbody.velocity = new Vector2(moveX * speed.x, Mathf.Min(rigidbody.velocity.y, 0));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
    }
}
