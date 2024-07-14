using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueContainer : ScriptableObject
{
    public List<DialogueLine> lines; // 대화의 각 줄을 저장하는 리스트
}
