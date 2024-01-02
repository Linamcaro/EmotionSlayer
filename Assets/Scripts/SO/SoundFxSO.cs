using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound",menuName ="Sounds/SoundFx")]
public class SoundFxSO : ScriptableObject
{

    public AudioClip footsteps;
    public AudioClip jump;
    public AudioClip monsterDeath;
    public AudioClip monsterRoar;
    public AudioClip orbPickup;
    public AudioClip swordSlash;

}
