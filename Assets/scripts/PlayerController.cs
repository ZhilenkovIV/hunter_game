using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private KeyCode JumpKey = KeyCode.UpArrow;
    private Rigidbody2D rb;
    private Animator anim;

    public float maxSpeed = 10f;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;

    //находится ли персонаж на земле или в прыжке?
    public bool isGrounded = false;
    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    public Transform groundCheck;
    public GameObject lamp;
    public GameObject stroke;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.1f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;
    public bool canMove = true;
    public bool canUseLamp = false;


    public IEnumerator disabledControl(float delta) {
        canMove = false;
        yield return new WaitForSeconds(delta);
        canMove = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<TakeDamage>().damageAction = (n) =>
        {
            if (canMove)
            {
                Fight2D.recoil(rb, n.GetComponent<Rigidbody2D>().position, 15);
                StartCoroutine(disabledControl(0.1f));
            }
        };
        //если персонаж на земле и нажат пробел...
        GetComponent<Jump>().trigger = () => isGrounded && Input.GetKeyDown(JumpKey) && canMove;
        GetComponent<Jump>().stopJump = () => !Input.GetKey(JumpKey);

        EventPickUp.PickUp += (s) =>
        {
            switch (s)
            {
                case "lamp":
                    canUseLamp = true;
                    break;
            }
        };
    }

    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
	private void FixedUpdate()
    {
        //определяем, на земле ли персонаж
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //устанавливаем соответствующую переменную в аниматоре
        anim.SetBool("Ground", isGrounded);
        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", rb.velocity.y);

        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        float move = Input.GetAxis("Horizontal");


        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости
        if (canMove)
        {
            rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
        }
        //rb.AddForce(new Vector2(move * maxSpeed, 0));
        //если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
        //if (!isGrounded)
        //    return;

        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (move > 0 && !isFacingRight)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (move < 0 && isFacingRight)
            Flip();

    }

    private void Update()
    {
        if (canUseLamp && Input.GetKeyDown(KeyCode.C)) {
            lamp.SetActive(!lamp.activeSelf);
            GetComponent<Jump>().enabled = !lamp.activeSelf;
            stroke.GetComponent<PlayerStroke>().isActive = !lamp.activeSelf;
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
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
