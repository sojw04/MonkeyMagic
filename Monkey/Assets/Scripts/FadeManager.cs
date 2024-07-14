using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// FadeManager 클래스는 씬 전환 시 페이드 인/아웃 효과를 관리합니다.
/// 이 클래스는 DialogueManager와 상호작용하여 대화 시작 전후에 페이드 효과를 적용합니다.
/// </summary>
public class FadeManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private bool isStartScene = false;
    [SerializeField] private bool isForestScene = false;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private DialogueComponent dialogueComponent;
    private bool isFading = false;
    private string sceneToLoad;

    private void Start()
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.gameObject.SetActive(true);
            if (!isStartScene)
            {
                fadeAnimator.SetTrigger("FadeIn");
            }
        }
        else
        {
            Debug.LogError("Fade Animator is not assigned.");
        }

        if (isForestScene && dialogueManager != null)
        {
            dialogueManager.PrepareDialogue(playerSprite, dialogueComponent);
        }
    }

    private void Update()
    {
        if (isStartScene && Input.GetKeyDown(KeyCode.Return) && !isFading)
        {
            LoadSceneWithFade("ForestScene");
        }
    }

    /// <summary>
    /// 페이드 아웃 효과와 함께 씬을 로드합니다.
    /// </summary>
    /// <param name="sceneName">로드할 씬 이름</param>
    public void LoadSceneWithFade(string sceneName)
    {
        if (!isFading)
        {
            sceneToLoad = sceneName;
            if (fadeAnimator != null)
            {
                fadeAnimator.SetTrigger("FadeOut");
                isFading = true;
            }
            else
            {
                Debug.LogError("Fade Animator is not assigned.");
            }
        }
    }

    /// <summary>
    /// 페이드 인이 완료되었을 때 호출됩니다.
    /// </summary>
    public void OnFadeInComplete()
    {
        Debug.Log("Fade In Complete");
        if (isForestScene && dialogueManager != null)
        {
            dialogueManager.StartDialogue(playerSprite, dialogueComponent);
        }
    }

    /// <summary>
    /// 페이드 아웃이 완료되었을 때 호출됩니다.
    /// </summary>
    public void OnFadeOutComplete()
    {
        Debug.Log("Fade Out Complete");
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        isFading = false;
    }
}
