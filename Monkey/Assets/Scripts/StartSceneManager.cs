using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            fadeManager.LoadSceneWithFade("ForestScene");
        }
    }
}
