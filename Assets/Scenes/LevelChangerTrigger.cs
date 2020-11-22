using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangerTrigger : MonoBehaviour
{
    public LevelChanger levelChanger;
    public int levelToLoad;
    public Vector2 direction;
    public Vector3 newPlayerPosition;
    private PlayerController controller;
    private Rigidbody2D playerRB;
    private float dirX;


    private void Start()
    {
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Debug.Log(playerCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("enter + direction:" + direction.x);
            controller = collision.GetComponent<PlayerController>();
            playerRB = controller.GetComponent<Rigidbody2D>();

            dirX = Mathf.Sign(GetComponent<Collider2D>().bounds.center.x - collision.transform.position.x);
            collision.GetComponent<PlayerController>().CanInputHandle = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("stay + direction:" + direction.x);
            ICommand move;
            //move = new MoveXCommand(playerRB, direction.x, controller.maxSpeed);
            move = new MoveXCommand(playerRB, dirX * controller.maxSpeed);
            move.Execute();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("exit + direction:" + direction.x);
            if (dirX == direction.x)
            {
                levelChanger.FadeToLevel(levelToLoad, newPlayerPosition);
            }
            else
            {
                controller.CanInputHandle = true;
            }
        }
    }
}
