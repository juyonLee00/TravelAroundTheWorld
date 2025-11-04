using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimationFunc : MonoBehaviour
{
    [SerializeField] TalkManager talkManager;

    public void OnAnimationEnd()
    {
        talkManager.isAnimationPlaying = false;

        if (talkManager.currentNode != null && talkManager.currentNode.nextNodes.Count > 0)
        {
            talkManager.currentNode = talkManager.currentNode.nextNodes[0];
        }
        else
        {
            UnityEngine.Debug.LogWarning("No next dialogue node found");
        }
        SoundManager.Instance.PlaySFX("twinkle");
        talkManager.invitationText.gameObject.SetActive(true);
    }

    public void OnTrainAnimationEnd()
    {
        talkManager.isAnimationPlaying = false;
    }
}
