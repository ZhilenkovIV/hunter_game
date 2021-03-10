using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private string checkObjectName = "GroundCheck";

    //находится ли персонаж на земле или в прыжке?

    public bool OnGround {   get; private set;   }

    public System.Action GroundIn;
    public System.Action GroundOut;

    private bool WasGround;

    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    private Transform groundCheck;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.1f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find(checkObjectName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WasGround = OnGround;
        OnGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (!WasGround && OnGround) {
            GroundIn?.Invoke();
        }
        if (WasGround && !OnGround) {
            GroundOut?.Invoke();
        }
    }
}
