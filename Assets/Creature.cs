using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public LegController left;
    public LegController right;

    public GameObject head;

    private Vector3 initialPosition;

    public Genome genome;

    public void Update()
    {
        //left.position = Mathf.Sin(Time.time);
        //right.position = Mathf.Sin(-Time.time);
        
        left.position = genome.left.EvaluateAt(Time.time);
        right.position = genome.right.EvaluateAt(Time.time);
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
