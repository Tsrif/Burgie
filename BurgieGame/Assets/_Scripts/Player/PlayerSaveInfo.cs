using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class PlayerSaveInfo : SerializedScriptableObject
{
    [SerializeField]
    public Dictionary<string, float> levelAndTime = new Dictionary<string, float>();
    //Death Count?
    public int totalDeaths;
    //Total Time?
    public string levelTime;
}
