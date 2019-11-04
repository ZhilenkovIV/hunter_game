using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectScript : MonoBehaviour
{
    public Sprite damageSprite;
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<TakeDamage>().SetDeadAction((n)=>{
            GetComponent<SpriteRenderer>().sprite = damageSprite;
        });
    }

}
