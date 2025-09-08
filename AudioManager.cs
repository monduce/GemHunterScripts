using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource seSourcePrefab;

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] seClips;

    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> seDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        foreach (var clip in bgmClips)
            bgmDict[clip.name] = clip;

        foreach (var clip in seClips)
            seDict[clip.name] = clip;
    }

    public void PlayBGM(string name)
    {
        if (bgmDict.ContainsKey(name))
        {
            bgmSource.clip = bgmDict[name];
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        if (seDict.ContainsKey(name))
        {
            AudioSource seSource = Instantiate(seSourcePrefab, transform);
            seSource.clip = seDict[name];
            seSource.Play();
            Destroy(seSource.gameObject, seSource.clip.length);
        }
    }
}
