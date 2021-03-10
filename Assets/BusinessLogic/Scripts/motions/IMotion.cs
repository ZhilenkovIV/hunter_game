using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMotion : ICommand
{
    void SetSpeed(Vector2 speed);

    void SetSpeedX(float speedX);
    void SetSpeedY(float speedY);

    void Suspend();
    void Resume();

    void Suspend(float time);

}
