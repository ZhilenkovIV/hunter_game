using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxSpeed = 10f;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;

    public string targetTag;

    [HideInInspector]
    public Rigidbody2D target;

    public Vector2 visibilityRect;
    //public Vector2 lostRect;
    public float stopDistX = 0;
    public bool canMove = true;

    public ICommand attackCommand;


    public float attackRadius;

    public IEnumerator disabledControl(float time) {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        GetComponentInChildren<ZombiPunch>().attackObject = target.GetComponent<Transform>();
        GetComponent<TakeDamage>().damageAction +=
            (n)=> {
                Fight2D.recoil(GetComponent<Rigidbody2D>(), n.GetComponent<Rigidbody2D>().position, 15);
                StartCoroutine(disabledControl(0.1f));
            };
        attackCommand = GetComponentInChildren<ZombiPunch>();
    }

    private void FixedUpdate()
    {
        if (target && canMove)
        {
            Vector2 sub = target.position - rb.position;

            if (Mathf.Abs(sub.x) > stopDistX && Mathf.Abs(sub.x) < visibilityRect.x && Mathf.Abs(sub.y) < visibilityRect.y) {
                float move = Mathf.Sign(sub.x);
                ICommand motion = new MoveXCommand(rb, move, maxSpeed);
                motion.Execute();
            }

            GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            //если нажали клавишу для перемещения вправо, а персонаж направлен влево
            if (rb.velocity.x > 0 && !isFacingRight)
                //отражаем персонажа вправо
                Flip();
            //обратная ситуация. отражаем персонажа влево
            else if (rb.velocity.x < 0 && isFacingRight)
                Flip();

            if ((rb.position - target.position).magnitude < 2 * attackRadius)
            {
                attackCommand.Execute();
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
        Gizmos.DrawWireCube(transform.position, visibilityRect);
        Gizmos.DrawLine(transform.position - stopDistX * Vector3.right, transform.position + stopDistX * Vector3.right);
    }

}
