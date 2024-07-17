using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxVelocity : MonoBehaviour
{
    public float maxYVelocity = 3f;
    public float minYVelocity = -3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.y > maxYVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, maxYVelocity);
        }

        if (this.GetComponent<Rigidbody2D>().velocity.y < minYVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, minYVelocity);
        }
    }
}
