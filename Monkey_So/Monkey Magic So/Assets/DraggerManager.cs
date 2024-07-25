using UnityEngine;

public class DraggerManager : MonoBehaviour
{
    private static DraggerManager _instance;
    public static DraggerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DraggerManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("DraggerManager");
                    _instance = manager.AddComponent<DraggerManager>();
                }
            }
            return _instance;
        }
    }

    private Dragger _currentDragger;

    public void StartDragging(Dragger dragger)
    {
        if (_currentDragger != null)
        {
            _currentDragger.StopDragging();
        }

        _currentDragger = dragger;
        _currentDragger.StartDragging();
    }

    public void StopDragging()
    {
        if (_currentDragger != null)
        {
            _currentDragger.StopDragging();
            _currentDragger = null;
        }
    }
}
