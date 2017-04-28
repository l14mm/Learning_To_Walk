using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome : MonoBehaviour {

    //public GenomeLeg leg1;
    //public GenomeLeg leg2;
    public GenomeLeg[] legs = new GenomeLeg[2];

    public Genome Clone()
    {
        Genome genome = new Genome();
        genome.legs = new GenomeLeg[legs.Length];
        for (int i = 0; i < legs.Length; i++)
        {
            genome.legs[i] = legs[i].Clone();
        }
        //genome.leg1 = leg1.Clone();
        //genome.leg2 = leg2.Clone();
        //Debug.Log("cloned m: " + genome.left.m + ", old m: " + left.m);
        return genome;
    }

    public Genome Mutate()
    {
        float val = Random.Range(0f, legs.Length);
        
        for (int i = 0; i < legs.Length; i++)
        {
            if (val >= i)
                legs[i].Mutate();
            //Debug.Log("mutating leg " + i);
        }

        return this;
    }
}
