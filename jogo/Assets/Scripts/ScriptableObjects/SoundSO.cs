using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Sound")]
public class SoundSO : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    public SoundType type;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
    public bool loop = false;
}