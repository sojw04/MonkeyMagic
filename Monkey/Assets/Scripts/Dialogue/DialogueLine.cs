using UnityEngine;

/// <summary>
/// DialogueLine 클래스는 대화의 한 줄을 나타냅니다.
/// 이 클래스는 DialogueContainer에서 사용됩니다.
/// </summary>

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/DialogueLine")]
public class DialogueLine : ScriptableObject
{
    public string characterName; // 대화하는 캐릭터의 이름
    public string text; // 대화 텍스트
    public Sprite characterImage1; // 첫 번째 캐릭터 이미지
    public Sprite characterImage2; // 두 번째 캐릭터 이미지
}
