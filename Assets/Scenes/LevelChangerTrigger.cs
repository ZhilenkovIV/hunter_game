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
    private Vector2 enterDistance;


    private void Start()
    {
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Debug.Log(playerCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller = collision.GetComponent<PlayerController>();
            playerRB = controller.GetComponent<Rigidbody2D>();
            enterDistance = transform.position - controller.transform.position;
            enterDistance.x = Mathf.Sign(enterDistance.x);
            enterDistance.y = Mathf.Sign(enterDistance.y);
            collision.GetComponent<PlayerController>().CanInputHandle = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ICommand move;
            if (direction.x != 0)
            {
                move = new MoveXCommand(playerRB, enterDistance.x * controller.maxSpeed);
                move.Execute();
            }
            if (direction.y > 0) {
                controller.jumpCommand.Execute();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (enterDistance.x == direction.x || enterDistance.y == direction.y)
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
