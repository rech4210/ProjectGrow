using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingEffect : MonoBehaviour
{

    List<GameObject> ChainLighting;
    List<Transform> EnemyTransform = new List<Transform>();

    [SerializeField]
    public LineRenderer Lighting;
    [SerializeField]
    public GameObject LightCircle;
    [SerializeField]
    public Transform[] tCircle;

    bool bisTurnOn;
    List<Vector2> monstertransform = new List<Vector2>();

    List<GameObject> Circlelist = new List<GameObject>();

    private void Start()
    {
        AttackLighting(tCircle);
    }

    public void AttackLighting(Transform[] transforms)
    {
        foreach (var i in transforms)
        {
            monstertransform.Add(i.position);
        }

        LineRenderer line = Instantiate(Lighting);
        line.SetWidth(0, 0);

        var tmp = Instantiate(LightCircle, this.gameObject.transform);
       //tmp.SetActive(false);
        Circlelist.Add(tmp);

        line.positionCount = monstertransform.Count + 1;
        line.SetPosition(0, this.gameObject.transform.position);

        for (int i = 0; i < monstertransform.Count; i++)
        {
            tmp = Instantiate(LightCircle, transforms[i]);
            tmp.SetActive(false);
            Circlelist.Add(tmp);
            line.SetPosition(i + 1, monstertransform[i]);
        }

        StartCoroutine(LightingCoroutine(line));
    }

    IEnumerator LightingCoroutine(LineRenderer line)
    {
        for (float i = 0; i < 0.05f; i += Time.deltaTime)
        {
            line.SetWidth(i, i);
            foreach(var cir in Circlelist)
            {
                Color tmp = cir.GetComponent<SpriteRenderer>().color;
                tmp.a += i;
                cir.GetComponent<SpriteRenderer>().color = tmp;
            }
            yield return null;
        }

        //foreach (var i in Circlelist) i.SetActive(true);

        for (float i = 1; i > 0f; i -= Time.deltaTime)
        {
            line.SetWidth(i, i);
            yield return null;
        }

        //foreach (var i in Circlelist) i.SetActive(false);
    }

    public void TurnOn()
    {
        bisTurnOn = true;
    }

    public void TurnOff()
    {
        bisTurnOn = false;
    }


}