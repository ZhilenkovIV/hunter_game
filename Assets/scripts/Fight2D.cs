using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight2D : MonoBehaviour
{


	public static void recoil(Rigidbody2D current, Vector2 sourceRecoil, float power) {
		Vector2 dist = current.position - sourceRecoil;
		if (dist.x < 0)
		{
			current.AddForce(new Vector2(-power, 0), ForceMode2D.Impulse);
		}
		else {
			current.AddForce(new Vector2(power, 0), ForceMode2D.Impulse);
		}
	}
}

