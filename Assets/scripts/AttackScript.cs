using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public string attackTag;
    private SpriteRenderer spriteRendOther;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(attackTag))
        {
            Debug.Log("attack");
            Collider2D thisCollider = GetComponent<BoxCollider2D>();
            spriteRendOther = other.gameObject.GetComponent<SpriteRenderer>();
            spriteRendOther.color = Color.red;
            Invoke("returnColor", 0.2f);
            PlayerCntrl script = GetComponentInParent<PlayerCntrl>();
            if (script != null)
            {
                Debug.Log("script");
                script.rebound(other.bounds.center, new Vector2(1.5f,0));
            }
            else {
                Debug.Log("null pointer");
            }
        }
    }


    void returnColor() {
        spriteRendOther.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
