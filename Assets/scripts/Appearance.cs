using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    public GameObject appearPrefab;
    public GameObject unitPrefab;
    private RectTransform areaAppearance;
    public float period;
    private float minPeriod;
    public int maxUnits;
    public float delay;
    private float currentTime = 0;
    public int nUnits = 0;
    private int deadUnitsCnt = 0;


    // Start is called before the first frame update
    void Start()
    {
        areaAppearance = GetComponent<RectTransform>();
        minPeriod = period / 3;

    }

    private IEnumerator createUnit() {
        Vector3 pointAppearance = new Vector3(Random.Range(areaAppearance.offsetMin.x, areaAppearance.offsetMax.x),
                Random.Range(areaAppearance.offsetMin.y, areaAppearance.offsetMax.y), 0);

        GameObject appearance = Instantiate(appearPrefab, pointAppearance, Quaternion.identity);
        nUnits++;

        yield return new WaitForSeconds(delay);
        Destroy(appearance);

        GameObject zombi = Instantiate(unitPrefab, pointAppearance, Quaternion.identity);
        zombi.GetComponent<TakeDamage>().SetDeadAction((n) => {
            nUnits--;
            deadUnitsCnt++;
            if (deadUnitsCnt % 5 == 0) {
                period *= 0.8f;
                period = Mathf.Max(period, minPeriod);
            }
        });
        zombi.GetComponent<TakeDamage>().AddDeadAction((n) => Destroy(zombi));
    }

    // Update is called once per frame
    void Update()
    {
        if (nUnits < maxUnits && currentTime > period)
        {
            currentTime = 0;
            StartCoroutine(createUnit());
        } else {
            currentTime += Time.deltaTime;
        }
    }
}
