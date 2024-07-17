using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxY : MonoBehaviour
{
    public float maxY = 100f;
    public float minY = -100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.transform.position.y > maxY)
        {
            this.transform.position = new Vector3(this.transform.position.x, maxY, 0);
        }
        if (this.transform.position.y < minY)
        {
            this.transform.position = new Vector3(this.transform.position.x, minY, 0);
        }
    }
}
