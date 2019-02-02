using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureNew : MonoBehaviour
{
    public List<HingeJoint> legs = new List<HingeJoint>();

    public GameObject head;
    private Vector3 initialPosition;

    public struct Gene
    {
        public float a, b, c, d;

        public void Init()
        {
            this.a = Random.Range(-1f, 1f);
            this.b = Random.Range(-1f, 1f);
            this.c = Random.Range(-1f, 1f);
            this.d = Random.Range(-1f, 1f);
        }

        public Gene Mutate()
        {
            Gene gene = new Gene();
            
            gene.a = this.a;
            gene.b = this.b;
            gene.c = this.c;
            gene.d = this.d;

            float mutationScale = 2;
            float rangeScale = 4;
            switch (Random.Range(0, 3 + 1))
            {
                case 0:
                    gene.a += Random.Range(-0.1f, 0.1f) * mutationScale;
                    gene.a = Mathf.Clamp(a, -1f * rangeScale, +1f * rangeScale);
                    break;
                case 1:
                    gene.b += Random.Range(-0.1f, 0.1f) * mutationScale;
                    gene.b = Mathf.Clamp(b, -1f * rangeScale, +1f * rangeScale);
                    break;
                case 2:
                    gene.c += Random.Range(-0.25f, 0.25f) * mutationScale;
                    gene.c = Mathf.Clamp(c, 0.1f * rangeScale, 2f * rangeScale);
                    break;
                case 3:
                    gene.d += Random.Range(-0.25f, 0.25f) * mutationScale;
                    gene.d = Mathf.Clamp(d, -2f * rangeScale, 2f * rangeScale);
                    break;
            }

            return gene;
        }
    }

    public float GetFitness()
    {
        // Walking score
        float walkingScore = Vector3.Distance(initialPosition, head.transform.position);

        // Balancing score - favor creatures heavily based on if they stay upright
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
