using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenomeLeg : MonoBehaviour {

    public float m;
    public float M;
    public float o;
    public float p;

    public float EvaluateAt(float time)
    {
        return (M - m) / 2 * (1 + Mathf.Sin((time + o) * Mathf.PI * 2 / p)) + m;
    }

    public GenomeLeg Clone()
    {
        GenomeLeg leg = new GenomeLeg();
        leg.m = m;
        leg.M = M;
        leg.o = o;
        leg.p = p;
        return leg;
    }

    public void Mutate()
    {
        float mutationScale = 5;
        switch (Random.Range(0, 3 + 1))
        {
            case 0:
                m += Random.Range(-0.1f, 0.1f) * mutationScale;
                m = Mathf.Clamp(m, -1f, +1f);
                break;
            case 1:
                M += Random.Range(-0.1f, 0.1f) * mutationScale;
                M = Mathf.Clamp(M, -1f, +1f);
                break;
            case 2:
                p += Random.Range(-0.25f, 0.25f) * mutationScale;
                p = Mathf.Clamp(p, 0.1f, 2f);
                break;
            case 3:
                o += Random.Range(-0.25f, 0.25f) * mutationScale;
                o = Mathf.Clamp(o, -2f, 2f);
                break;
        }
        //Debug.Log("values: " + "m: " + m);
    }
}
