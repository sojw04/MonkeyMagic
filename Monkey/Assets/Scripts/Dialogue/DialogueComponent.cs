using UnityEngine;
using System.Collections.Generic;


/// DialogueComponent 클래스는 특정 스프라이트와 대응하는 대화 컨테이너 관리
/// DialogueManager와 상호작용하여 플레이어의 스프라이트에 따라 알맞는 대화 컨테이너 제공

[System.Serializable]
public class DialogueCondition
{
    public Sprite sprite; // 비교할 스프라이트
    public DialogueContainer container; // 스프라이트와 매핑된 대화 컨테이너
}

public class DialogueComponent : MonoBehaviour
{
    public List<DialogueCondition> dialogueConditions; // 스프라이트와 대화 컨테이너의 리스트

    public DialogueContainer GetContainerForSprite(Sprite playerSprite)
    {
        foreach (var condition in dialogueConditions)
        {
            if (condition.sprite == playerSprite)
            {
                return condition.container;
            }
        }
        return null;
    }
}
