using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueNode
{
    public ProDialogue data;
    public List<DialogueNode> nextNodes;
    public int nodeId;
    public bool isChoiceNode;

    public DialogueNode(ProDialogue data, int id)
    {
        this.data = data;
        nodeId = id;
        nextNodes = new List<DialogueNode>();
    }

    public void AddNext(DialogueNode next)
    {
        if (next != null)
            nextNodes.Add(next);
    }
    
    public DialogueNode GetNextNode()
    {
        foreach (var next in nextNodes)
        {
            //퀘스트 매니저 보고 추후 조건 추가 예정
            if (next.data.quest == "")
                return next;
        }

        return nextNodes.Count > 0 ? nextNodes[0] : null;
    }
}
