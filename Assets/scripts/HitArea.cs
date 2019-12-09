using UnityEngine;

public class HitArea : MonoBehaviour
{
    public int hitValue = 1;
    public Vector2 size;
    private BoxCollider2D box;
    public Transform parent;

    //public delegate void AttackAction(TakeDamage.DamageInfo info);
    public delegate void AttackAction(GameObject other);
    private AttackAction attackAction;

    public struct AttackInfo {
        Vector2 position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (parent == null) {
            parent = transform;
        }
        box = gameObject.AddComponent<BoxCollider2D>();
        box.size = size;
        box.isTrigger = true;
        if (attackAction == null) {
            attackAction = (n) => { };
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        TakeDamage takeDamage = col.gameObject.GetComponent<TakeDamage>();
        if (takeDamage != null && takeDamage.damage(hitValue, parent.gameObject))
        {
            attackAction(col.gameObject);
        }
    }

    public void AddHitAction(AttackAction action) {
        attackAction += action;
    }

}
