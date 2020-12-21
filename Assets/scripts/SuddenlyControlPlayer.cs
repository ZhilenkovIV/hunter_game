using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenlyControlPlayer : MonoBehaviour
{
    private PlayerController controller;
    public MoveXCommand moveX;

    // Start is called before the first frame update
    void Start()
    {
        moveX = controller.GetComponent<MoveXCommand>();
    }

    private IEnumerator takeOver() {

        controller.CanInputHandle = false;
        moveX.Direction = -1;
        for (float time = 0; time < 0.5f; time += Time.deltaTime) {
            moveX.Execute();
            yield return null;
        }
        while (!controller.IsGrounded)
        {
            yield return null;
        }
        controller.jumpCommand.Execute();
        yield return null;
        moveX.Direction = 1;
        for (float time = 0; time < 0.5f; time += Time.deltaTime)
        {
            moveX.Execute();
            yield return null;
        }
        while (!controller.IsGrounded) {
            yield return null;
        }
        controller.jumpCommand.Execute();
        yield return null;
        moveX.Direction = -1;
        for (float time = 0; time < 0.5f; time += Time.deltaTime)
        {
            moveX.Execute();
            yield return null;
        }
        float timeWait = 0.2f;
        moveX.Direction = 1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = -1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = 1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = -1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = 1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = -1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = 1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = -1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = 1;
        moveX.Execute();
        yield return new WaitForSeconds(timeWait);
        moveX.Direction = -1;
        moveX.Execute();
        yield return null;
        controller.CanInputHandle = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            controller = collision.GetComponent<PlayerController>();
            StartCoroutine(takeOver());
        }
    }
}
