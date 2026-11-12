using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Event Listening")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEVent;
    public FloatEventSO volumeEvent;
    public VoidEventSO pauseEvent;

    [Header("Events Broadcasting")]
    public FloatEventSO syncVolumeEvent;

    [Header("Components")]
    public AudioSource BGMSource;
    public AudioSource FXSource;

    public AudioMixer mixer;

    private void OnEnable()
    {
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEVent.OnEventRaised += OnBGMEvent;
        volumeEvent.OnEventRaised += OnVolumeEvent;
        pauseEvent.OnEventRaised += OnPauseEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEVent.OnEventRaised -= OnBGMEvent;
        volumeEvent.OnEventRaised -= OnVolumeEvent;
        pauseEvent.OnEventRaised -= OnPauseEvent;
    }

    private void OnPauseEvent()
    {
        float amount;
        mixer.GetFloat("MasterVolume", out amount);
        syncVolumeEvent.RaiseEvent(amount);
    }

    private void OnVolumeEvent(float value)
    {
        mixer.SetFloat("MasterVolume", value * 100 - 80);
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
    
    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }
}
