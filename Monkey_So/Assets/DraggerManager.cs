using UnityEngine;

public class DraggerManager : MonoBehaviour
{
    public static DraggerManager Instance { get; private set; }

    private Dragger _currentDragger;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDragging(Dragger dragger)
    {
        if (_currentDragger != null && _currentDragger != dragger)
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

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            StopDragging();
        }
    }
}
