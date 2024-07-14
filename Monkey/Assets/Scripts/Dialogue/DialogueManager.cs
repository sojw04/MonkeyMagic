using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// DialogueManager 클래스는 대화 UI를 관리하고 대화를 제어합니다.
/// 이 클래스는 DialogueComponent, DialogueContainer와 상호작용하여 대화를 시작하고 진행합니다.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text characterNameText;
    [SerializeField] private Image characterImage1;
    [SerializeField] private Image characterImage2;
    [SerializeField] private FadeManager fadeManager;
    private DialogueContainer dialogueContainer;
    private int currentLineIndex = 0;

    private void Start()
    {
        dialoguePanel.SetActive(false); // 초기에는 대화 패널을 비활성화
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            ShowNextLine(); // 대화가 진행 중이고 Enter 키가 눌리면 다음 대화 줄을 표시
        }
    }

    /// <summary>
    /// 대화를 준비합니다.
    /// </summary>
    /// <param name="playerSprite">플레이어의 스프라이트</param>
    /// <param name="dialogueComponent">DialogueComponent 인스턴스</param>
    public void PrepareDialogue(Sprite playerSprite, DialogueComponent dialogueComponent)
    {
        dialogueContainer = dialogueComponent.GetContainerForSprite(playerSprite);
        if (dialogueContainer != null)
        {
            currentLineIndex = 0;
        }
        else
        {
            Debug.LogError("No suitable Dialogue Container found for the given sprite.");
        }
    }

    /// <summary>
    /// 대화를 시작합니다.
    /// </summary>
    /// <param name="playerSprite">플레이어의 스프라이트</param>
    /// <param name="dialogueComponent">DialogueComponent 인스턴스</param>
    public void StartDialogue(Sprite playerSprite, DialogueComponent dialogueComponent)
    {
        dialogueContainer = dialogueComponent.GetContainerForSprite(playerSprite);
        if (dialogueContainer != null)
        {
            Debug.Log("Starting dialogue with container: " + dialogueContainer.name);
            currentLineIndex = 0;
            dialoguePanel.SetActive(true);
            ShowNextLine();
        }
        else
        {
            Debug.LogError("No suitable Dialogue Container found for the given sprite.");
        }
    }

    /// <summary>
    /// 다음 대화 줄을 표시합니다.
    /// </summary>
    private void ShowNextLine()
    {
        if (dialogueContainer == null || dialogueText == null || characterNameText == null)
        {
            Debug.LogError("One or more references are null.");
            return;
        }

        if (currentLineIndex < dialogueContainer.lines.Count)
        {
            DialogueLine line = dialogueContainer.lines[currentLineIndex];
            if (line == null)
            {
                Debug.LogError($"Dialogue Line at index {currentLineIndex} is null.");
                return;
            }

            characterNameText.text = line.characterName;
            dialogueText.text = line.text;

            // 첫 번째 캐릭터 이미지 설정
            if (characterImage1 != null)
            {
                if (line.characterImage1 != null)
                {
                    characterImage1.sprite = line.characterImage1;
                    characterImage1.enabled = true;
                }
                else
                {
                    characterImage1.enabled = false;
                }
            }

            // 두 번째 캐릭터 이미지 설정
            if (characterImage2 != null)
            {
                if (line.characterImage2 != null)
                {
                    characterImage2.sprite = line.characterImage2;
                    characterImage2.enabled = true;
                }
                else
                {
                    characterImage2.enabled = false;
                }
            }

            currentLineIndex++;
        }
        else
        {
            EndDialogue(); // 대화가 끝나면 대화를 종료
        }
    }

    /// <summary>
    /// 대화를 종료합니다.
    /// </summary>
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");
        if (fadeManager != null)
        {
            fadeManager.LoadSceneWithFade("GameScene");
        }
    }
}
