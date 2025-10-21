using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    private static AudioLibrary _instance;

    public static AudioLibrary Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<AudioLibrary>("AudioLibrary");
            return _instance;
        }
    }

    [Header("🧍 Sons Tesla")] public SoundSO somPasso;
    public SoundSO teslaSomTiro;
    public SoundSO teslaSomDash;
    public SoundSO teslaSomAtaque;
    public SoundSO teslaSomMachucado;
    public SoundSO teslaSomCorrendo;
    public SoundSO teslaSomBobinaAtiva;
}
    