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
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerRB = controller.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller.CanInputHandle = false;
            enterDistance = transform.position - controller.transform.position;
            enterDistance.x = Mathf.Sign(enterDistance.x);
            enterDistance.y = Mathf.Sign(enterDistance.y);
            Debug.Log("enter");
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
            Debug.Log("exit");
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
