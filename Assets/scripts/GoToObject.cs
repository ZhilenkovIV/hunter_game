using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToObject : MonoBehaviour
{
    public string followObjectTag;
    private Rigidbody2D followObject;
    public Vector2 speed;
    public Vector2 acceleration;
    private Rigidbody2D rb;
    private bool canChange = true;
    public Vector2 visibleRadius;
    private SpriteRenderer sprite;
    public bool onlyX = true;
    private Vector2 currentSpeed;

    public float waitTime;
    private float currentTime;
    private Vector2 returnPoint;
    private bool isReturning;


    public void recoil(GameObject other, Vector2 power, float timeBlockMotion)
    {
        Vector2 pointFrom = other.transform.position;
        Vector2 dir = (pointFrom - rb.position);
        dir.x = (dir.x > 0) ? 1 : -1;
        dir.y = 1;
        rb.velocity = dir * speed * power;
        BlockMovement(timeBlockMotion);
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        returnPoint = rb.position;
        GameObject objectGO = GameObject.FindGameObjectWithTag(followObjectTag);
        followObject = objectGO.GetComponent<Rigidbody2D>();
        if (acceleration.Equals(Vector2.zero)) {
            acceleration = new Vector2(100, 100);
        }
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

    private void motionToPoint(Vector2 point) {
        float delta = Time.deltaTime;
        Vector2 distance = point - rb.position;
        sprite.flipX = point.x < 0;
        float signX = Mathf.Sign(distance.x);
        currentSpeed.x += acceleration.x * signX * delta;
        currentSpeed.x = (Mathf.Abs(currentSpeed.x) > speed.x) ? speed.x * signX : currentSpeed.x;
        if (!onlyX)
        {
            float signY = Mathf.Sign(distance.y);
            currentSpeed.y += acceleration.y * Mathf.Sign(distance.y) * delta;
            currentSpeed.y = (Mathf.Abs(currentSpeed.y) > speed.y) ? speed.y * signY : currentSpeed.y;
        }
        rb.velocity = currentSpeed;
    }

    private void brake()
    {

        float delta = Time.deltaTime;
        float signX = Mathf.Sign(currentSpeed.x);
        currentSpeed.x -= acceleration.x * signX * delta;
        if (signX != Mathf.Sign(currentSpeed.x))
        {
            currentSpeed.x = 0;
        }


        if (!onlyX)
        {
            float signY = Mathf.Sign(currentSpeed.y);
            currentSpeed.y -= acceleration.y * signY * delta;
            if (signY != Mathf.Sign(currentSpeed.y))
            {
                currentSpeed.y = 0;
            }
        }

        rb.velocity = currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            Vector2 distance = followObject.position - rb.position;
            float delta = Time.deltaTime;
            if (Mathf.Abs(distance.x) < visibleRadius.x &&
                Mathf.Abs(distance.y) < visibleRadius.y)
            {
                motionToPoint(followObject.position);
            }
            else {
                if (rb.velocity != Vector2.zero)
                {
                    brake();
                }

            }
        }
    }
}
