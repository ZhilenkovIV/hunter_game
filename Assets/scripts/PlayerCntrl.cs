using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{

    Rigidbody2D rigidbody;
    SpriteRenderer sprite;
    Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        speed = new Vector2(10,24);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = UnityEngine.Input.GetAxis("Horizontal");
        if (moveX > 0)
        {
            sprite.flipX = false;
        }
        else if(moveX < 0){
            sprite.flipX = true;
        }
        rigidbody.MovePosition(rigidbody.position + Vector2.right * moveX * speed.x * Time.deltaTime);

        /*float moveY = UnityEngine.Input.GetAxis("Vertical");
        if (rigidbody.velocity.y == 0) {
            rigidbody.MovePosition(rigidbody.position + Vector2.up * moveY * speed.y * Time.deltaTime);
        }*/
    }
}
