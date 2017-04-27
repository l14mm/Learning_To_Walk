using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour {

    public GenomeLeg left;
    public GenomeLeg right;

    public Genome Clone()
    {
        Genome genome = new Genome();
        genome.left = left.Clone();
        genome.right = right.Clone();
        //Debug.Log("cloned m: " + genome.left.m + ", old m: " + left.m);
        return genome;
    }

    public Genome Mutate()
    {
        if (Random.Range(0f, 1f) > 0.5f)
            left.Mutate();
        else
            right.Mutate();

        return this;
    }
}
