using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public LayerMask layerMask;
    public float hit = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int layerColl = collision.collider.gameObject.layer;
        if (((1 << layerColl) & layerMask.value) != 0)
        {
            //collision.collider.GetComponent<TakeDamage>().damage(this);
        }
    }
}
