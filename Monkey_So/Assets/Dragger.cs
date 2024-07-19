using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Vector3 _originalPosition;
    private Camera _cam;
    [SerializeField] private float _speed = 10f;
    private bool _isDragging = false;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private GameObject _currentHoverObject;

    void Awake()
    {
        _cam = Camera.main;
        _originalPosition = transform.position; // ������Ʈ�� ���� ��ġ ����
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (_isDragging)
        {
            Interact();
        }
        else
        {
            DraggerManager.Instance.StartDragging(this);
        }
    }

    void Update()
    {
        if (_isDragging)
        {
            DragObject();
        }

        // ��Ŭ�� �� ������Ʈ�� ���� ��ġ�� ����
        if (Input.GetMouseButtonDown(1))
        {
            ReturnToOriginalPosition();
        }
    }

    public void StartDragging()
    {
        _isDragging = true;
        _dragOffset = transform.position - GetMousePos();
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true; 
        }
        if (_collider != null)
        {
            _collider.enabled = false; 
        }
    }

    public void StopDragging()
    {
        _isDragging = false;
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false; // ������ ��ȣ�ۿ� �簳
        }
        if (_collider != null)
        {
            _collider.enabled = true; // �浹 �簳
        }
    }

    void ReturnToOriginalPosition()
    {
        transform.position = _originalPosition;
        StopDragging();
    }

    void DragObject()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
    }

    void Interact()
    {
        if (_currentHoverObject != null)
        {
            if (_currentHoverObject.CompareTag("Wire"))
            {
                if (gameObject.CompareTag("Nippers"))
                {
                    Debug.Log("Valid object!");
                }
                else
                {
                    Debug.Log("Invalid object!");
                }
            }
        }
        else
        {
            Debug.Log("No object to interact with.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wire"))
        {
            _currentHoverObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wire"))
        {
            _currentHoverObject = null;
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = _cam.WorldToScreenPoint(transform.position).z; // ������Ʈ�� z ��ǥ�� ����
        return _cam.ScreenToWorldPoint(mousePos);
    }
}
