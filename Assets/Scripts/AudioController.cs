using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] AudioMixer _mixer;

    List<AudioSource> _musicSources = new List<AudioSource>();
    List<AudioSource> _sfxSources = new List<AudioSource>();
    [SerializeField] AudioSource _mainMusicSource, _secondMusicSource;
    int _musicSourceIndex = 0;
    int _sfxSourceIndex = 0;
    bool _musicCrossfade = false;
    float _musicCrossfadeTime = 1;

    float _lastMusicVolume, _lastSfxVolume;

    AudioClipsScriptable _audioDB;
    [SerializeField] AudioMixerGroup _musicMixerGroup, _sfxMixerGroup;

    public delegate void ChangeSoundVolume();
    public event ChangeSoundVolume soundVolumeIsChanged;

    private void Awake()
    {
        InitializeComponent();
    }
    public void InitializeComponent()
    {
        if (!_mixer)
        {
            _mixer = Resources.Load<AudioMixer>("MainMixer");
            _audioDB = Resources.Load<AudioClipsScriptable>("AudioClipDB");
            _musicMixerGroup = _mixer.FindMatchingGroups("Music")[0];
            _sfxMixerGroup = _mixer.FindMatchingGroups("Sfx")[0];

            for (int i = 0; i < 2; i++)
            {
                AudioSource _musicSource = gameObject.AddComponent<AudioSource>();
                _musicSource.outputAudioMixerGroup = _musicMixerGroup;
                _musicSource.loop = true;
                _musicSources.Add(_musicSource);

                if (i == 1)
                {
                    _musicSource.volume = 0;
                }
            }

            _mainMusicSource = _musicSources[0];

            for (int i = 0; i < 5; i++)
            {
                AudioSource _sfxSource = gameObject.AddComponent<AudioSource>();
                _sfxSource.outputAudioMixerGroup = _sfxMixerGroup;
                _sfxSources.Add(_sfxSource);
            }
        }

    }

    private void Update()
    {
        if (_musicCrossfade)
        {
            if (_mainMusicSource.volume != 1 || _secondMusicSource.volume != 0)
            {
                _mainMusicSource.volume = Mathf.MoveTowards(_mainMusicSource.volume, 1, _musicCrossfadeTime * Time.deltaTime);
                _secondMusicSource.volume = Mathf.MoveTowards(_secondMusicSource.volume, 0, _musicCrossfadeTime * Time.deltaTime);
            }

            else
            {
                _musicCrossfade = false;
            }
        }
    }
    public void PlayMainMusic(string _clipName)
    {
        if (_mainMusicSource.clip != GetAudioClipByName(_clipName))
        {
            MusicCrossfade();

            _mainMusicSource.loop = true;
            _mainMusicSource.clip = GetAudioClipByName(_clipName);
            _mainMusicSource.Play();
        }
    }

    public void StopMainMusic()
    {
        MusicCrossfade();
        _mainMusicSource.loop = false;
        _mainMusicSource.clip = GetAudioClipByName("Silence");
        _mainMusicSource.Play();
    }

    public void PlayMainSfx(string _clipName)
    {
        _sfxSourceIndex++;

        if (_sfxSourceIndex > _sfxSources.Count - 1)
            _sfxSourceIndex = 0;

        _sfxSources[_sfxSourceIndex].PlayOneShot(GetAudioClipByName(_clipName));
    }

    public void PlaySfx(AudioSource _source, string _clipName, bool _loop)
    {
        if (!_loop)
            _source.PlayOneShot(GetAudioClipByName(_clipName));
        else
        {
            _source.loop = _loop;
            _source.clip = GetAudioClipByName(_clipName);
            _source.Play();
        }
    }
    public void PlayMusic(AudioSource _source, string _clipName, bool _loop = false)
    {
        _source.loop = _loop;
        _source.clip = GetAudioClipByName(_clipName);
        _source.Play();
    }
    public void ChangeVolume(bool _music, float _newVolume)
    {
        _mixer.SetFloat(_music ? "MusicVolume" : "SfxVolume", _newVolume);
        soundVolumeIsChanged?.Invoke();
    }
    public float GetVolume(bool _music)
    {
        float _volume;
        _mixer.GetFloat(_music ? "MusicVolume" : "SfxVolume", out _volume);
        return _volume;
    }

    public void Mute(bool _music)
    {
        if (_music)
            _lastMusicVolume = GetVolume(_music);
        else
            _lastSfxVolume = GetVolume(_music);

        ChangeVolume(_music, -80);
    }

    public void Unmute(bool _music)
    {
        ChangeVolume(_music, _music ? _lastMusicVolume : _lastSfxVolume);
    }

    public bool IsMuted(bool _music)
    {
        return GetVolume(_music) != -80 ? false : true;
    }
    public void MusicCrossfade()
    {
        _secondMusicSource = _musicSources[_musicSourceIndex];

        if (_secondMusicSource.isPlaying)
        {
            _musicSourceIndex++;

            if (_musicSourceIndex > _musicSources.Count - 1)
                _musicSourceIndex = 0;

            _musicCrossfade = true;
        }

        _mainMusicSource = _musicSources[_musicSourceIndex];
    }

    public AudioClip GetAudioClipByName(string _audioClipName)
    {
        if (_audioDB.Clips.Exists(x => x.Id == _audioClipName))
        {
            return _audioDB.Clips[_audioDB.Clips.FindIndex(x => x.Id == _audioClipName)].Clip;
        }

        return null;
    }

}
