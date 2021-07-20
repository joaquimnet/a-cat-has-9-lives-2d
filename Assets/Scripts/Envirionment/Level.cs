using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string levelName;
    public string defaultBGM;
    public AmbienceTypes ambience;

    [SerializeField] private string id;

    public enum AmbienceTypes
    {
        NORMAL,
        DARK,
        DREAM
    }

    [ContextMenu("Generate Id")]
    void GenerateId() => id = System.Guid.NewGuid().ToString();
}
