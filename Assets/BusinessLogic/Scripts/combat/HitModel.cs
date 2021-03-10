using UnityEngine;
using System.Collections;

public class HitModel : MonoBehaviour
{
    public float hit;
    public LayerMask hitLayer;

    public System.Action<TakeDamageModel> HitAction;
}
