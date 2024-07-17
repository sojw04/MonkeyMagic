using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashFood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CUP")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().mass = 1f;
        }

    }
}
