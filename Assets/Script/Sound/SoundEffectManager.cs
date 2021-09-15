﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            changeableSounds.Add(BGMAudioSource);
        }
    }

    [SerializeField]
    AudioSource UIAudioSource;
    [SerializeField]
    AudioSource BGMAudioSource;
    [SerializeField]
    AudioSource EffectAudioSource;

    readonly List<AudioSource> changeableSounds = new List<AudioSource>();

    float pitch;
    float pitchOld;

    public float Pitch { get => pitch; set => pitch = value; }

    public void Join(AudioSource source)
    {
        changeableSounds.Add(source);
    }

    public void Leave(AudioSource source)
    {
        changeableSounds.Remove(source);
    }

    /// <summary>
    /// 音のピッチを変更する
    /// </summary>
    /// <param name="pitch"></param>
    public void SetSoundPitchToAll(float pitch)
    {
        foreach (var item in changeableSounds)
        {
            if (item)
            {
                item.pitch = pitch;
            }
        }
    }

    #region SOUNDS
    [Header("各効果音")]
    [SerializeField]
    AudioSource ShootSoundPrefab;
    public AudioSource MakeShootSound()
    {
        return Instantiate(ShootSoundPrefab);
    }
    #endregion

    private void Update()
    {
        if (pitch != pitchOld)
        {
            pitchOld = pitch;
            SetSoundPitchToAll(pitch);
        }
    }

}
