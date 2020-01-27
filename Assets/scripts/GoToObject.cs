using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToObject : MonoBehaviour
{
    public string targetTag;
    private Rigidbody2D targetObject;
    public Vector2 speed;
    public Vector2 acceleration;
    private Rigidbody2D rb;
    private bool canChange = true;
    public Vector2 visibleRadius;
    public float minDistance;
    public float persecutionRadius;
    private SpriteRenderer sprite;
    private Vector2 currentSpeed;

    private static float ERROR = 0.5f;

    private bool targetIsVisible;
    private Vector2 standPosition;
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
        standPosition = rb.position;
        sprite = GetComponent<SpriteRenderer>();
        GameObject objectGO = GameObject.FindGameObjectWithTag(targetTag);
        targetObject = objectGO.GetComponent<Rigidbody2D>();
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

    private void motionToDirection(Vector2 distance) {
        float delta = Time.deltaTime;
        sprite.flipX = distance.x < 0;
        float signX = Mathf.Sign(distance.x);
        currentSpeed.x += acceleration.x * signX * delta;
        currentSpeed.x = (Mathf.Abs(currentSpeed.x) > speed.x) ? speed.x * signX : currentSpeed.x;
        float signY = Mathf.Sign(distance.y);
        currentSpeed.y += acceleration.y * Mathf.Sign(distance.y) * delta;
        currentSpeed.y = (Mathf.Abs(currentSpeed.y) > speed.y) ? speed.y * signY : currentSpeed.y;
        rb.velocity = currentSpeed;
    }

    private void brake()
    {
        if (rb.velocity == Vector2.zero) {
            return;
        }
        float delta = Time.deltaTime;
        float signX = Mathf.Sign(currentSpeed.x);
        currentSpeed.x -= acceleration.x * signX * delta;
        if (signX != Mathf.Sign(currentSpeed.x))
        {
            currentSpeed.x = 0;
        }

        float signY = Mathf.Sign(currentSpeed.y);
        currentSpeed.y -= acceleration.y * signY * delta;
        if (signY != Mathf.Sign(currentSpeed.y))
        {
            currentSpeed.y = 0;
        }

        rb.velocity = currentSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            Vector2 distanceTarget = targetObject.position - rb.position;
            Vector2 distanceStand = standPosition - rb.position;
            if (Mathf.Abs(distanceTarget.x) < visibleRadius.x &&
                Mathf.Abs(distanceTarget.y) < visibleRadius.y) {
                targetIsVisible = true;
            }
            else if (distanceStand.magnitude > persecutionRadius)
            {
                targetIsVisible = false;
                isReturning = true;
            }

            Vector2 currentDistance;
            if (!targetIsVisible || isReturning)
            {
                currentDistance = distanceStand;
            }
            else {
                currentDistance = distanceTarget;
            }

            if (currentDistance.magnitude < ERROR)
            {
                brake();
                isReturning = false;
            }
            else
            {
                if (currentDistance.magnitude > minDistance)
                {
                    motionToDirection(currentDistance);
                }
            }
        }
    }
}
