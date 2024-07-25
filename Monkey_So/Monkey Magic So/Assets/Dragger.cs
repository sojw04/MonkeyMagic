using UnityEngine;
using UnityEngine.UI;

public class Dragger : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Vector3 _originalPosition;
    private Camera _cam;
    [SerializeField] private float _speed = 10f;
    private bool _isDragging = false;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private GameObject _currentHoverObject;
    [SerializeField] private GameObject clearUI; // Clear UI ������Ʈ

    private string puzzleState = "incomplete"; // puzzle2 ���� ����

    void Awake()
    {
        _cam = Camera.main;
        _originalPosition = transform.position;
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_collider == null)
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _collider.isTrigger = true; // Ʈ���ŷ� ����
            Debug.LogWarning("Collider2D�� ��� BoxCollider2D�� �߰��߽��ϴ�.");
        }

        if (_rigidbody == null)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody.isKinematic = true; // �߷��� ������ ���� �ʵ��� ����
            Debug.LogWarning("Rigidbody2D�� ��� �߰��߽��ϴ�.");
        }

        if (clearUI != null)
        {
            clearUI.SetActive(false); // Clear UI�� ��Ȱ��ȭ
        }
    }

    void OnMouseDown()
    {
        if (puzzleState == "clear") return;

        Debug.Log("OnMouseDown ȣ���");
        if (!_isDragging)
        {
            StartDragging();
        }
    }

    void Update()
    {
        if (puzzleState == "clear") return;

        if (_isDragging)
        {
            DragObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ReturnToOriginalPosition();
        }

        if (_currentHoverObject != null)
        {
            Debug.Log("���� ȣ�� ���� ������Ʈ��: " + _currentHoverObject.name);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        if (puzzleState == "clear") return;

        CheckForHoverObject();
    }

    void CheckForHoverObject()
    {
        // �浹�ϴ� ��� �ݶ��̴��� ������ �迭 ����
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0);

        // ���� ȣ�� ���� ������Ʈ �ʱ�ȭ
        _currentHoverObject = null;

        // �浹�ϴ� �ݶ��̴� �� Wire �±׸� ���� ������Ʈ�� ã��
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Wire"))
            {
                _currentHoverObject = hitCollider.gameObject;
                Debug.Log("Wire �±� ������Ʈ�� �浹 ��: " + _currentHoverObject.name);
                break;
            }
        }
    }

    public void StartDragging()
    {
        Debug.Log("StartDragging ȣ���");
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
        Debug.Log("StopDragging ȣ���");
        _isDragging = false;
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = false;
        }
        if (_collider != null)
        {
            _collider.enabled = true;
        }
    }

    void ReturnToOriginalPosition()
    {
        Debug.Log("ReturnToOriginalPosition ȣ���");
        transform.position = _originalPosition;
        StopDragging();
    }

    void DragObject()
    {
        Debug.Log("DragObject ȣ���");
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
    }

    void Interact()
    {
        Debug.Log("Interact ȣ���");
        if (_currentHoverObject != null)
        {
            Debug.Log("���� ȣ�� ���� ������Ʈ: " + _currentHoverObject.name);
            if (_currentHoverObject.CompareTag("Wire"))
            {
                Debug.Log("ȣ�� ���� ������Ʈ�� Wire �±׸� ������ �ֽ��ϴ�.");
                if (gameObject.CompareTag("Nippers"))
                {
                    Debug.Log("���� �巡�� ���� ������Ʈ�� Nippers �±׸� ������ �ֽ��ϴ�.");
                    Debug.Log("�ùٸ� ������Ʈ�Դϴ�!");
                    ClearPuzzle(); // puzzle2�� clear ���·� ����
                }
                else
                {
                    Debug.Log("���� �巡�� ���� ������Ʈ�� Nippers �±׸� ������ ���� �ʽ��ϴ�.");
                    ReturnToOriginalPosition();
                }
            }
            else
            {
                Debug.Log("ȣ�� ���� ������Ʈ�� Wire �±׸� ������ ���� �ʽ��ϴ�.");
                ReturnToOriginalPosition();
            }
        }
        else
        {
            Debug.Log("��ȣ�ۿ��� ������Ʈ�� �����ϴ�.");
        }
    }

    void ClearPuzzle()
    {
        Debug.Log("��� �׼��� ����Ǿ����ϴ�. Puzzle2�� Ŭ����Ǿ����ϴ�.");
        puzzleState = "clear"; // puzzle2 ���¸� clear�� ����
        Time.timeScale = 0; // ���� �ð� ����

        if (clearUI != null)
        {
            clearUI.SetActive(true); // Clear UI Ȱ��ȭ
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (puzzleState == "clear") return;

        Debug.Log("OnTriggerEnter2D ȣ���");
        if (other.CompareTag("Wire"))
        {
            Debug.Log("Wire �±� ������Ʈ�� �浹��");
            _currentHoverObject = other.gameObject;
        }
        else
        {
            Debug.Log("�浹�� ������Ʈ�� Wire �±׸� ������ ���� �ʽ��ϴ�.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (puzzleState == "clear") return;

        Debug.Log("OnTriggerExit2D ȣ���");
        if (other.CompareTag("Wire"))
        {
            Debug.Log("Wire �±� ������Ʈ�� �浹 ����");
            _currentHoverObject = null;
        }
        else
        {
            Debug.Log("�浹 ����� ������Ʈ�� Wire �±׸� ������ ���� �ʽ��ϴ�.");
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = _cam.WorldToScreenPoint(transform.position).z;
        return _cam.ScreenToWorldPoint(mousePos);
    }
}
