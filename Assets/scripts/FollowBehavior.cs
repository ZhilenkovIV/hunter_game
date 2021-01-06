using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
    public string targetTag = "Player";
    private Rigidbody2D rb;
    private MoveXCommand motion;
    public float maxSpeed;
    public bool isFollowing = false;
    private bool isFacingRight;

    public Vector2 zoneOfDetecting;
    public Vector2 minDistance;
    public Vector2 zoneOfLosting;

    private Rect zoneOfDetectingRect;
    private Rect minDistanceRect;
    private Rect zoneOfLostingRect;

    public Rigidbody2D target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Rigidbody2D>();
        motion = GetComponent<MoveXCommand>();
        rb = GetComponent<Rigidbody2D>();
        zoneOfDetectingRect = new Rect(rb.position, zoneOfDetecting);
        minDistanceRect = new Rect(rb.position, minDistance);
        zoneOfLostingRect = new Rect(rb.position, zoneOfLosting);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            zoneOfDetectingRect.center = rb.position;
            zoneOfLostingRect.center = rb.position;
            minDistanceRect.center = rb.position;

            Vector2 distance = target.position - rb.position;
            if (!isFollowing && zoneOfDetectingRect.Contains(target.position)) {
                isFollowing = true;
            }

            if (isFollowing && !minDistanceRect.Contains(target.position)) {
                
                motion.speed = Mathf.Sign(distance.x) * maxSpeed;
                motion.Execute();
            }

            if (isFollowing && !zoneOfLostingRect.Contains(target.position))
            {
                isFollowing = false;
            }

            //если нажали клавишу для перемещения вправо, а персонаж направлен влево
            if (distance.x > 0 && !isFacingRight)
                //отражаем персонажа вправо
                Flip();
            //обратная ситуация. отражаем персонажа влево
            else if (distance.x < 0 && isFacingRight)
                Flip();
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
        Gizmos.DrawWireCube(transform.position, zoneOfDetecting);
        Gizmos.DrawWireCube(transform.position, minDistance);
        Gizmos.DrawWireCube(transform.position, zoneOfLosting);
    }
}
