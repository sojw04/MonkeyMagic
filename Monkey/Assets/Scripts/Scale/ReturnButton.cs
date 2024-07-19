using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        SceneManager.LoadScene("Scale_Sample", LoadSceneMode.Additive);
    }
}
