using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapManagement : MonoBehaviour
{
    public LevelCreator levelCreator;
    float levelLength;
    public Image fillingLap;
    public RectTransform cursorLap;
    public RectTransform startLap;
    public RectTransform endLap;

    private void Start()
    {
        levelLength = levelCreator.levelData.levelChunks.Length * 20 + 20;
    }

    void Update()
    {
        float i_lapPercentage = PlayerController.pc.body.position.z / levelLength;
        fillingLap.fillAmount = i_lapPercentage;
        cursorLap.anchoredPosition = new Vector2(Mathf.Lerp(startLap.anchoredPosition.x, endLap.anchoredPosition.x, i_lapPercentage), cursorLap.anchoredPosition.y);
    }
}
