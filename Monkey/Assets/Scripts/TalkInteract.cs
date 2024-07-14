using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TalkInteract 클래스는 플레이어가 특정 오브젝트와 상호작용할 때 대화를 시작합니다.
/// 이 클래스는 DialogueManager와 DialogueComponent와 상호작용하여 대화를 제어합니다.
/// </summary>
public class TalkInteract : MonoBehaviour
{
    [SerializeField] private Image interactionImage; // 충돌 시 표시할 이미지
    [SerializeField] private DialogueComponent dialogueComponent; // DialogueComponent를 사용하여 대화 컨테이너 관리
    private DialogueManager dialogueManager;
    private bool isPlayerInRange = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager not found in the scene.");
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
            if (dialogueManager != null && dialogueComponent != null)
            {
                // 플레이어의 스프라이트를 가져와서 대화 시작
                Sprite playerSprite = GetPlayerSprite();
                dialogueManager.StartDialogue(playerSprite, dialogueComponent);
            }
            else
            {
                Debug.LogError("DialogueManager or DialogueComponent is null.");
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
        }
    }

    private Sprite GetPlayerSprite()
    {
        // Player 오브젝트에서 SpriteRenderer 컴포넌트를 찾아 스프라이트를 반환합니다.
        // 실제 구현에서는 Player 오브젝트의 위치나 접근 방식을 기반으로 구현해야 합니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                return spriteRenderer.sprite;
            }
        }
        return null;
    }
}
