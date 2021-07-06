using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode jumpButton = KeyCode.Space;
    public KeyCode attackButton = KeyCode.Z;
    public KeyCode lampButton = KeyCode.C;
    private Rigidbody2D rb;
    private Animator anim;

    public float maxSpeed;

    private GroundCheck groundCheck;

    [SerializeField]
    private bool canInputHandle = true;
    private bool isProgramBlock = false; //для функции disableInput, чтобы она не возвращал контроль при блокировке
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
            isProgramBlock = !value;
        }
    }

    //public System.Action Grounded;

    private delegate bool LampButtonIsPressed();
    private LampButtonIsPressed lampButtonIsPressed;

    [HideInInspector]
    public ICommand jumpCommand;
    [HideInInspector]
    public ICommand lampCommand;
    [HideInInspector]
    public ICommand attackCommand;
    [HideInInspector]
    public IMotion motion;

    public IEnumerator disableInput(float deltaTime){
        canInputHandle = false;
        yield return new WaitForSeconds(deltaTime);
        canInputHandle = true && !isProgramBlock;
    }


    // Start is called before the first frame update
    void Start()
    {

        attackCommand = GetComponent<PlayerAttack>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        groundCheck = GetComponent<GroundCheck>();


        //jumpCommand = new JumpCommand(rb, jumpHeight);
        jumpCommand = GetComponent<JumpCommand>();
        //lampCommand = GetComponent<LampCommand>();
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        motion = GetComponent<MoveXCommand>();
        lampCommand = GetComponent<LampCommand>();


        lampButtonIsPressed = () => Input.GetKeyDown(lampButton);
        //lampButtonIsPressed = () => false;


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

        if (canInputHandle)
        {
            motion.SetSpeedX(Input.GetAxis("Horizontal") * maxSpeed);
            motion.Execute();
        }

        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        //устанавливаем соответствующую переменную в аниматоре
        //anim.SetBool("Ground", isGrounded);
        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", rb.velocity.y);

    }

    private void Update()
    {
        if (canInputHandle)
        {
            bool lampIsActive = (lampCommand as LampCommand).lamp.activeSelf;
            if (lampButtonIsPressed())
            {
                if (!lampIsActive)
                    lampCommand.Execute();
                else
                    lampCommand.Undo();
            }

            if (groundCheck.OnGround && Input.GetKeyDown(jumpButton) && !lampIsActive)
            {
                jumpCommand.Execute();
            }
            else if (Input.GetKeyUp(jumpButton))
            {
                jumpCommand.Undo();
            }

            if (Input.GetKeyDown(attackButton) && !lampIsActive)
            {
                attackCommand.Execute();
            }
        }
    }

}
