using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public LevelData levelData;

    void Start()
    {
        //INTRO
        Instantiate(levelData.levelChunkStart, Vector3.zero, Quaternion.identity, transform);

        for (int i = 0; i < levelData.levelChunks.Length+1; i++)
        {
            //MIDDLE
            if (i != levelData.levelChunks.Length)
            {
                Instantiate(levelData.levelChunks[i], new Vector3(0, 0, (i + 1) * 20), Quaternion.identity, transform);
            }
            //END
            else
            {
                Instantiate(levelData.levelChunkEnd, new Vector3(0, 0, (i + 1) * 20), Quaternion.identity, transform);
            }
        }
    }
}
