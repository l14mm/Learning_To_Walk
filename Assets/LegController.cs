using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    private HingeJoint hinge;

    public float outerExtent;
    public float innerExtent;

    [Range(-1, +1)]
    public float position = +1;

    JointSpring spring;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        spring = hinge.spring;
    }

    void FixedUpdate()
    {
        spring.targetPosition = Scale(-1F, 1F, innerExtent, outerExtent, position);
        hinge.spring = spring;
    }

    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
