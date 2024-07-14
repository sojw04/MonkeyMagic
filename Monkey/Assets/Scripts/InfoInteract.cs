using UnityEngine;
using UnityEngine.UI;


/// 플레이어가 특정 오브젝트와 상호작용할 때 정보를 표시합니다.
/// 상호작용 범위 내에 플레이어가 들어왔을 때 정보 패널 활성화

public class InfoInteract : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Image interactionImage; // 충돌 시 표시할 이미지
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (infoPanel == null)
        {
            Debug.LogError("InfoPanel is not assigned.");
        }

        if (interactionImage != null)
        {
            interactionImage.gameObject.SetActive(false); // 초기에는 이미지를 비활성화
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed and player is in range.");
            if (infoPanel != null)
            {
                infoPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("InfoPanel is null.");
            }
        }

        // 이미지 위치를 오브젝트 상단에 고정
        if (isPlayerInRange && interactionImage != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            interactionImage.transform.position = screenPosition + new Vector3(0, 80, 0); // 오브젝트 상단에 위치 조정
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered interaction range.");
            isPlayerInRange = true;
            if (interactionImage != null)
            {
                interactionImage.gameObject.SetActive(true); // 이미지 활성화
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited interaction range.");
            isPlayerInRange = false;
            if (interactionImage != null)
            {
                interactionImage.gameObject.SetActive(false); // 이미지 비활성화
            }

            if (infoPanel != null)
            {
                infoPanel.SetActive(false); // 플레이어가 범위를 벗어나면 정보 패널 비활성화
            }
        }
    }
}
