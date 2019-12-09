using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToObject : MonoBehaviour
{
    public Transform followObject;
    public Vector2 speed;
    private Rigidbody2D rb;
    private bool canChange = true;
    public Vector2 visibleRadius;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private IEnumerator BlockMovementEnum(float time)
    {
        canChange = false;
        yield return new WaitForSeconds(time);
        canChange = true;
    }

    public void BlockMovement(float time)
    {
        StartCoroutine(BlockMovementEnum(time));
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            float distanceX = followObject.position.x - transform.position.x;
            if (Mathf.Abs(distanceX) < visibleRadius.x)
            {
                sprite.flipX = distanceX < 0;
                if (distanceX > 0)
                {
                    rb.velocity = new Vector2(speed.x, 0);
                }
                else
                {
                    rb.velocity = new Vector2(-speed.x, 0);
                }
            }
        }
    }
}
