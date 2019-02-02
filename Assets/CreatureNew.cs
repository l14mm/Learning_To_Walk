using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureNew : MonoBehaviour
{
    public List<HingeJoint> legs = new List<HingeJoint>();

    public GameObject head;
    private Vector3 initialPosition;

    // public float a, b, c, d;
    public struct Gene
    {
        public float a, b, c, d;

        public Gene Mutate()
        {
            Gene gene = new Gene();
            gene.a = this.a + Random.Range(0, 0.1f);
            gene.b = this.b + Random.Range(0, 0.1f);
            gene.c = this.c + Random.Range(0, 0.1f);
            gene.d = this.d + Random.Range(0, 0.1f);
            return gene;
        }
    }

    public float GetFitness()
    {
        return Vector3.Distance(initialPosition, head.transform.position);
    }

    public Gene gene = new Gene();

    private float outerExtent = 60;
    private float innerExtent = -60;

    [Range(-1, +1)]
    private float position = +1;

    void Awake()
    {
        initialPosition = head.transform.position;
        gene.a = Random.Range(0, 1f);
        gene.b = Random.Range(0, 1f);
        gene.c = Random.Range(0, 1f);
        gene.d = Random.Range(0, 1f);
        // a = Random.Range(0, 1f);
        // b = Random.Range(0, 1f);
        // c = Random.Range(0, 1f);
        // d = Random.Range(0, 1f);
    }

    public float EvaluateAt(float time)
    {
        return (gene.b - gene.a) / 2 * (1 + Mathf.Sin((time + gene.c) * Mathf.PI * 2 / gene.d)) + gene.a;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < legs.Count; i++)
        {
            JointSpring spring = legs[i].spring;
            spring.targetPosition = Scale(-1F, 1F, innerExtent, outerExtent, EvaluateAt(System.DateTime.Now.Millisecond));
            legs[i].spring = spring;
        }
    }

    float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
