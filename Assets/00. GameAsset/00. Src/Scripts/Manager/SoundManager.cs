using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> bgmClips;
    [SerializeField] private List<AudioClip> sfxClips;

    [Header("Settings")]
    [SerializeField] private int maxSameSFXPlayCount = 3; // 동시에 재생 가능한 최대 동일 SFX 수

    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, int> sfxPlayingCount = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (var clip in bgmClips)
                bgmDict[clip.name] = clip;

            foreach (var clip in sfxClips)
                sfxDict[clip.name] = clip;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(string clipName)
    {
        if (!sfxDict.TryGetValue(clipName, out AudioClip clip))
        {
            Debug.LogWarning($"SFX Clip '{clipName}' 이 없습니다.");
            return;
        }

        // 재생 중인 카운트 확인
        if (!sfxPlayingCount.ContainsKey(clipName))
            sfxPlayingCount[clipName] = 0;

        if (sfxPlayingCount[clipName] >= maxSameSFXPlayCount)
            return; // 제한 초과

        sfxPlayingCount[clipName]++;
        sfxSource.PlayOneShot(clip);
        StartCoroutine(Co_DecreaseSFXCount(clipName, clip.length));
    }

    private IEnumerator Co_DecreaseSFXCount(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (sfxPlayingCount.ContainsKey(clipName))
            sfxPlayingCount[clipName]--;
    }

    public void PlayBGM(string clipName)
    {
        if (bgmDict.TryGetValue(clipName, out AudioClip clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM Clip '{clipName}' 이 없습니다.");
        }
    }

    public void StopBGM() => bgmSource.Stop();

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
    }
}

