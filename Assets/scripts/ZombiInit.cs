using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiInit : MonoBehaviour
{
    private Rigidbody2D rb;
    bool activeScript;
    public Transform player;

    private void recoil(GameObject other, Vector2 power)
    {
        Vector2 pointFrom = other.transform.position;
        Vector2 dir = (pointFrom - rb.position);
        dir.x = (dir.x > 0) ? 1 : -1;
        dir.y = 1;
        GoToObject motion = GetComponent<GoToObject>();
        rb.velocity = dir * motion.speed * power;
        motion.BlockMovement(0.15f);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<TakeDamage>().AddDamageAction((n)=>recoil(n, new Vector2(-2f, 0f)));
    }


}
