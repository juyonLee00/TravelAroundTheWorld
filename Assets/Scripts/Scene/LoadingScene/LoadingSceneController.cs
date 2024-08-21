using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
            progressBar.value = progress;
        
         if (progressText != null)
            progressText.text = Mathf.Min(Mathf.FloorToInt(progress * 100), 100) + "%";
    }
}