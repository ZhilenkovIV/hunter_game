using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHit : HitModel
{
    private Collider2D coll;
    private ContactFilter2D filter;
    private Collider2D[] colls;

    // Start is called before the first frame update
    void Start()
    {
        filter.layerMask = hitLayer;
        coll = GetComponent<Collider2D>();

        colls = new Collider2D[10];

        HitAction += (n) => {
            Rigidbody2D targetRb = n.GetComponent<Rigidbody2D>();
            if (targetRb.velocity.y == 0)
            {
                targetRb.AddForce(8 * Vector2.up, ForceMode2D.Impulse);
            }
        };
    }

    private void Update()
    {
        int size = coll.OverlapCollider(filter, colls);
        for (int i = 0; i < size; i++) {
            Damageable target = colls[i].GetComponent<Damageable>();
            if (target != null) {
                Fight2D.Action(this, target);
            }
        }
    }

}
