using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
    public string targetTag = "Player";
    private Rigidbody2D rb;
    private IMotion motion;
    public float maxSpeed;

    public bool IsFollowing { get; private set; }
    public bool IsInMinDistance { get; private set; }
    private bool isFacingRight;

    public Bounds zoneOfDetecting;
    public Bounds minDistance;
    public Bounds zoneOfLosting;

    private Rigidbody2D target;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Rigidbody2D>();
        motion = GetComponent<IMotion>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {

            Vector2 distance = target.position - rb.position;
            if (!IsFollowing && zoneOfDetecting.Contains(distance)) {
                IsFollowing = true;
                Debug.Log("follow");
            }

            IsInMinDistance = minDistance.Contains(distance);

            if (canMove)
            {
                if (IsFollowing && !IsInMinDistance)
                {
                    Vector2 dir = new Vector2(Mathf.Sign(distance.x), Mathf.Sign(distance.y));
                    motion.SetSpeed(dir * maxSpeed);
                    motion.Execute();
                }

                //если нажали клавишу для перемещения вправо, а персонаж направлен влево
                if (distance.x > 0 && !isFacingRight)
                    //отражаем персонажа вправо
                    Flip();
                //обратная ситуация. отражаем персонажа влево
                else if (distance.x < 0 && isFacingRight)
                    Flip();
            }


            if (IsFollowing && !zoneOfLosting.Contains(distance))
            {
                IsFollowing = false;
                Debug.Log("not follow");
            }

        }
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(zoneOfDetecting.center.x, zoneOfDetecting.center.y, 0), zoneOfDetecting.size);
        Gizmos.DrawWireCube(transform.position + new Vector3(minDistance.center.x, minDistance.center.y, 0), minDistance.size);
        Gizmos.DrawWireCube(transform.position + new Vector3(zoneOfLosting.center.x, zoneOfLosting.center.y, 0), zoneOfLosting.size);
    }
}
