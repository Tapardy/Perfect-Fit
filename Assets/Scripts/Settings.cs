using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SavedSettingsAndHighScore", menuName = "SettingsAndHighScore")]
public class Settings : ScriptableObject
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public int highScore;
}
