using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    //public LegController leg1;
    //public LegController leg2;
    public List<LegController> legs = new List<LegController>();

    public GameObject head;

    private Vector3 initialPosition;

    public Genome genome;

    public void Update()
    {
        //left.position = Mathf.Sin(Time.time);
        //right.position = Mathf.Sin(-Time.time);

        //leg1.position = genome.leg1.EvaluateAt(Time.time);
        //leg2.position = genome.leg2.EvaluateAt(Time.time);
        for (int i = 0; i < genome.legs.Length; i++)
        {
            legs[i].position = genome.legs[i].EvaluateAt(Time.time);
        }
    }


    public void Start()
    {
        initialPosition = head.transform.position;
    }

    public float GetScore()
    {
        // Walking score
        float walkingScore = Vector3.Distance(initialPosition, head.transform.position);
        //Debug.Log("walking score: " + walkingScore);

        // Balancing score
        bool headUp =
            head.transform.eulerAngles.z < 0 + 30 ||
            head.transform.eulerAngles.z > 360 - 30;
        bool headDown =
            head.transform.eulerAngles.z > 180 - 45 &&
            head.transform.eulerAngles.z < 180 + 45;

        return
            walkingScore
            * (headUp ? 2f : 1f)    // Double score if UP
            * (headDown ? 0.5f : 1f)    // Half score if DOWN
            ;
    }
}
