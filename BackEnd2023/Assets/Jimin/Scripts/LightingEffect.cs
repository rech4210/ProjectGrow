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
        Instantiate(LightCircle, this.gameObject.transform);
        line.positionCount = monstertransform.Count + 1;
        line.SetPosition(0, this.gameObject.transform.position);


        for (int i = 0; i < monstertransform.Count; i++)
        {
            Instantiate(LightCircle, transforms[i]);
            line.SetPosition(i + 1, monstertransform[i]);
        }
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