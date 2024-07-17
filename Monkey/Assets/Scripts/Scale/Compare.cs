using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compare : MonoBehaviour
{
    public GameObject LeftHand, RightHand, Middle;
    private Rigidbody2D lrb, rrb, mrb;
    private float L, R;

    private void Start()
    {
        lrb = LeftHand.GetComponent<Rigidbody2D>();
        rrb = RightHand.GetComponent<Rigidbody2D>();
        mrb = Middle.GetComponent<Rigidbody2D>();
    }

    private void weightScan()
    {
        L = LeftHand.GetComponent<HandWeight>().mass;
        R = RightHand.GetComponent<HandWeight>().mass;
    }


    public void CompareWeight()
    {
        weightScan();
        lrb.mass = 1f;
        rrb.mass = 1f;
        mrb.mass = 1f;
        if (L > R)
        {
            lrb.mass = 30f;
        }
        if (R > L)
        {
            rrb.mass = 30f;
        }
        if (L == R)
        {
            //Middle.transform.position += Vector3.up;
            mrb.mass = 60f;
        }
    }
}
