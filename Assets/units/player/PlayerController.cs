using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static KeyCode jumpButton = KeyCode.Space;
    public static KeyCode attackButton = KeyCode.Z;
    public static KeyCode lampButton = KeyCode.C;
    private Rigidbody2D rb;
    private Animator anim;

    public float maxSpeed = 10f;
    public float jumpHeight = 20f;

    public bool usePos = true;
    public VectorValue pos;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;

    //находится ли персонаж на земле или в прыжке?
    [SerializeField]
    private bool isGrounded = false;

    public bool IsGrounded {
        get {
            return isGrounded;
        }
    }
    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    private Transform groundCheck;
    public GameObject lamp;
    public GameObject stroke;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.1f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;

    private bool canInputHandle = true;
    public bool CanInputHandle
    {
        get {
            return canInputHandle;
        }

        set {
            if (value == false)
            {
                rb.velocity *= Vector2.up;
                lampCommand.Undo();
                jumpCommand.Undo();
            }
            canInputHandle = value;
        }
    }

    public System.Action Grounded;

    private delegate bool LampButtonIsPressed();
    private LampButtonIsPressed lampButtonIsPressed;

    [HideInInspector]
    public ICommand jumpCommand;
    [HideInInspector]
    public ICommand lampCommand;
    [HideInInspector]
    public ICommand attackCommand;

    public IEnumerator disabledInput(float deltaTime){
        canInputHandle = false;
        yield return new WaitForSeconds(deltaTime);
        canInputHandle = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (usePos)
        {
            transform.position = pos.initialValue;
        }
        groundCheck = transform.Find("GroundCheck");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<TakeDamage>().damageAction += (n) =>
        {
            Fight2D.recoil(rb, n.GetComponent<Rigidbody2D>().position, 15);
            StartCoroutine(disabledInput(0.1f));
        };

        jumpCommand = new JumpCommand(rb, jumpHeight);
        lampCommand = new LampCommand(lamp);
        attackCommand = stroke.GetComponent<PlayerStroke>();

        lampButtonIsPressed = () => false;

        PickUpEvent.Action += (s) =>
        {
            switch (s)
            {
                case "lamp":
                    lampButtonIsPressed = () => Input.GetKeyDown(lampButton);
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

        if (canInputHandle)
        {
            //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
            //при стандартных настройках проекта 
            //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
            //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
            float move = Input.GetAxis("Horizontal");

            ICommand motion = new MoveXCommand(rb, move * maxSpeed);
            motion.Execute();


            if (isGrounded && Input.GetKeyDown(jumpButton) && !lamp.activeSelf)
            {
                jumpCommand.Execute();
            }
            else if (Input.GetKeyUp(jumpButton))
            {
                jumpCommand.Undo();
            }
        }

        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        //устанавливаем соответствующую переменную в аниматоре
        anim.SetBool("Ground", isGrounded);
        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", rb.velocity.y);

    }

    private void Update()
    {
        if (canInputHandle)
        {
            if (lampButtonIsPressed())
            {
                if (!lamp.activeSelf)
                    lampCommand.Execute();
                else
                    lampCommand.Undo();
            }

            if (Input.GetKeyDown(attackButton))
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
}
