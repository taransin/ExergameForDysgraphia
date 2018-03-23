using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongName", menuName = "Song", order = 1)]
public class Song : ScriptableObject {
    public AudioClip audio;
    public float offset;
    public float tempo;
}
