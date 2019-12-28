using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiInit : MonoBehaviour
{
    private Rigidbody2D rb;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<TakeDamage>().AddDamageAction((n)=>GetComponent<GoToObject>().recoil(n, new Vector2(-1.5f, 0f), 0.1f));
    }


}
