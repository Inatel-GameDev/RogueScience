using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : MonoBehaviour
{
    private AudioSource source;
    private AudioManager manager;

    public void Init(AudioManager m)
    {
        manager = m;
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
    }

    public void PlaySound(SoundSO sound)
    {
        if (sound == null) return;

        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        source.Play();

        if (!sound.loop)
            StartCoroutine(StopAfterClip());
    }

    private System.Collections.IEnumerator StopAfterClip()
    {
        yield return new WaitForSeconds(source.clip.length / Mathf.Abs(source.pitch));
        manager.ReturnToPool(this);
    }

    public void Stop()
    {
        source.Stop();
        manager.ReturnToPool(this);
    }
}