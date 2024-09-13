using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree
{
    public StoryDialogueNode rootNode; // 트리의 루트 노드
    public StoryDialogueNode currentNode; // 현재 대화 진행 포인터 노드

    public DialogueTree(StoryDialogueNode root)
    {
        rootNode = root;
        currentNode = rootNode;
    }

    // 현재 노드에서 첫 번째 자식 노드로 이동
    public void MoveToNextNode(int childIndex = 0)
    {
        if (currentNode.children.Count > childIndex)
        {
            currentNode = currentNode.children[childIndex];
        }
        else
        {
            Debug.LogWarning("No more dialogues to progress.");
        }
    }

    // 트리 시작으로 초기화
    public void ResetToRoot()
    {
        currentNode = rootNode;
    }
}
