using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEvent : MonoBehaviour
{
    public PlayerController player;
    public Animator mortician;
    public GameObject[] dissapearingBlocks;
    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private IEnumerator Action() {

        player.CanInputHandle = false;
        player.GetComponent<Rigidbody2D>().velocity *= Vector2.up;
        mortician.SetTrigger("dig");
        yield return new WaitForSeconds(2f);
        foreach (var b in dissapearingBlocks) {
            Destroy(b);
        }
        player.CanInputHandle = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coll.IsTouchingLayers()) {
            StartCoroutine(Action());
        }
    }
}
