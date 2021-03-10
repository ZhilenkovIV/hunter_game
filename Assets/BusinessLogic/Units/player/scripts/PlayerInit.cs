using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GroundCheck groundCheck = GetComponent<GroundCheck>();
        Animator anim = GetComponent<Animator>();
        anim.SetBool("Ground", groundCheck.OnGround);
        groundCheck.GroundOut += () => anim.SetBool("Ground", groundCheck.OnGround);
        groundCheck.GroundIn += () => anim.SetBool("Ground", groundCheck.OnGround);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        GetComponent<TakeDamageModel>().damageAction += (n) =>
        {
            rb.AddForce(15 * Mathf.Sign(n.transform.lossyScale.x) * Vector2.right, ForceMode2D.Impulse);
        };

    }
}
