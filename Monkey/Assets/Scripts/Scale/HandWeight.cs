using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandWeight : MonoBehaviour
{

    public float mass;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weight"))
        {
            this.mass += collision.GetComponent<Rigidbody2D>().mass;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weight"))
        {
            this.mass -= collision.GetComponent<Rigidbody2D>().mass;
        }
    }

}
