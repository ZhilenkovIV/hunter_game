using UnityEngine;
using System.Collections;

public class TakeDamageModel : MonoBehaviour
{
    [SerializeField]
    private float hp;
    private bool isAlive = true;
    public float immunityTime = 0.2f;
    public Color immunityColor = Color.red;
    private int unitLayer;
    public bool isImmunuted = false;

    public event System.Action dieAction;
    public System.Action<HitModel> damageAction;



    public void Start()
    {
        unitLayer = gameObject.layer;
    }

    private IEnumerator immunity(float time)
    {
        GetComponent<SpriteRenderer>().color = immunityColor;
        gameObject.layer = 13;
        isImmunuted = true;
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.layer = unitLayer;
        isImmunuted = false;
    }

    public float HealthPoints{

        get{
            return hp;
        }

        set {
            if (isAlive && !isImmunuted)
            {
                if (value <= hp)
                {
                    hp = value;
                    if (value <= 0)
                    {
                        hp = 0;
                        isAlive = false;
                        if (dieAction != null) {
                            dieAction();
                        }
                        else {
                            Destroy(gameObject);
                        }
                    }
                    StartCoroutine(immunity(immunityTime));
                }
            }
        }
    }
}
