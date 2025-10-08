using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Prefabs e configurações")]
    [SerializeField] private GameObject pooledAudioPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<PooledAudioSource> pool = new Queue<PooledAudioSource>();
    private List<PooledAudioSource> active = new List<PooledAudioSource>();

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Cria fonte de música
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        // Cria pool de sons
        for (int i = 0; i < initialPoolSize; i++) CreateNew();
    }

    private PooledAudioSource CreateNew()
    {
        var go = Instantiate(pooledAudioPrefab, transform);
        var p = go.GetComponent<PooledAudioSource>();
        p.Init(this);
        go.SetActive(false);
        pool.Enqueue(p);
        return p;
    }

    private PooledAudioSource GetSource()
    {
        if (pool.Count == 0) CreateNew();
        var s = pool.Dequeue();
        s.gameObject.SetActive(true);
        active.Add(s);
        return s;
    }

    internal void ReturnToPool(PooledAudioSource p)
    {
        p.gameObject.SetActive(false);
        active.Remove(p);
        pool.Enqueue(p);
    }

    // 🔊 Funções principais
    public void PlaySound(SoundSO sound)
    {
        if (sound.type == SoundType.Music)
        {
            PlayMusic(sound);
            return;
        }

        var src = GetSource();
        src.PlaySound(sound);
    }

    private void PlayMusic(SoundSO sound)
    {
        musicSource.clip = sound.clip;
        musicSource.volume = sound.volume;
        musicSource.pitch = sound.pitch;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
