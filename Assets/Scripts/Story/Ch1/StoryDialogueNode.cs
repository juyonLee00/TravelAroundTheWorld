using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDialogueNode : MonoBehaviour
{
    public StoryDialogue dialogue; // 대화 내용
    public List<StoryDialogueNode> children; // 자식 노드 리스트
    public int nodeId; // 현재 노드의 ID
    public string location; // 장소 (카페, 객실 등)

    public StoryDialogueNode(StoryDialogue dialogue, int nodeId, string location)
    {
        this.dialogue = dialogue;
        this.nodeId = nodeId;
        this.location = location;
        children = new List<StoryDialogueNode>();
    }

    // 자식 노드를 추가하는 메서드
    public void AddChild(StoryDialogueNode childNode)
    {
        children.Add(childNode);
    }

    // 특정 위치로 이동할 수 있는지 확인하는 메서드
    public bool CanMoveTo(string targetLocation)
    {
        return location == targetLocation;
    }
}
