using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight2D : MonoBehaviour
{
	public delegate void LambdaAction(GameObject gameObject);

	public static GameObject NearTarget(Vector3 position, Collider2D[] array) {
        Collider2D current = null;
        float dist = Mathf.Infinity;

        foreach (Collider2D coll in array) {
            float curDist = Vector3.Distance(position, coll.transform.position);
            if (curDist < dist) {
                current = coll;
                dist = curDist;
            }
        }

        return current?.gameObject;

    }

	// point - точка контакта
	// radius - радиус поражения
	// layerMask - номер слоя, с которым будет взаимодействие
	// damage - наносимый урон
	// allTargets - должны-ли получить урон все цели, попавшие в зону поражения
	public static bool Action(Vector2 point, float radius, LayerMask layerMask, DealDamage damage, bool allTargets)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, layerMask);
		bool flag = false;

		if (!allTargets)
		{
			GameObject obj = NearTarget(point, colliders);
			if (obj != null && obj.GetComponent<TakeDamage>())
			{
				obj.GetComponent<TakeDamage>().damage(damage);
				flag = true;
				return flag;
			}
		}

		foreach (Collider2D hit in colliders)
		{
			if (hit.GetComponent<TakeDamage>())
			{
				hit.GetComponent<TakeDamage>().damage(damage);
				flag = true;
			}
		}

		return flag;
	}

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

