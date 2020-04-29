using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public GameObject[] levelChunks;
    public GameObject levelChunkStart;
    public GameObject levelChunkEnd;
}
