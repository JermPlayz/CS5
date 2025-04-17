using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        SelectCharacter,
        SelectTile,
        SelectAction,
        Music_Menu,
        Music_Battle_Rain,
        Music_Battle_Thunder
    }

    [System.Serializable]

    public class Sound
    {
        public SoundType Type;
        public AudioClip Clip;

        [Range(0f, 1f)]
        public float Volume = 1f;

        [HideInInspector]
        public AudioSource Source;
    }

    public static AudioManager Instance;

    public Sound[] AllSounds;

    private Dictionary<SoundType, Sound> _soundDictionary = new Dictionary<SoundType, Sound>();
    private AudioSource _musicSource1;
    private AudioSource _musicSource2;

    private void Awake()
    {
        Instance = this;

        foreach(var s in AllSounds)
        {
            _soundDictionary[s.Type] = s;
        }
    }

    public SoundType SelectedSound;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Play(SelectedSound);
        }
    }

    public void Play(SoundType type)
    {
        if(!_soundDictionary.TryGetValue(type, out Sound s))
        {
            Debug.LogWarning($"Sound type {type} not found!");
            return;
        }

        var soundObj = new GameObject($"Sound_{type}");
        var audioSrc = soundObj.AddComponent<AudioSource>();

        audioSrc.clip = s.Clip;
        audioSrc.volume = s.Volume;

        audioSrc.Play();

        Destroy(soundObj, s.Clip.length);
    }

    public void ChangeMusic1(SoundType type)
    {
        if(!_soundDictionary.TryGetValue(type, out Sound track))
        {
            Debug.LogWarning($"Music track {type} no found!");
            return;
        }

        if(_musicSource1 == null)
        {
            var container = new GameObject("SoundTrackObj");
            _musicSource1 = container.AddComponent<AudioSource>();
            _musicSource1.loop = true;
        }

        _musicSource1.clip = track.Clip;
        _musicSource1.volume = track.Volume;
        _musicSource1.Play();
    }

    public void MuteMusic1()
    {
        _musicSource1.volume = 0f;
    }

    public void UnmuteMusic1()
    {
        _musicSource1.volume = 1f;
    }

    public void ChangeMusic2(SoundType type)
    {
        if(!_soundDictionary.TryGetValue(type, out Sound track))
        {
            Debug.LogWarning($"Music track {type} no found!");
            return;
        }

        if(_musicSource2 == null)
        {
            var container = new GameObject("SoundTrackObj");
            _musicSource2 = container.AddComponent<AudioSource>();
            _musicSource2.loop = true;
        }

        _musicSource2.clip = track.Clip;
        _musicSource2.volume = track.Volume;
        _musicSource2.Play();
    }

    public void MuteMusic2()
    {
        _musicSource2.volume = 0f;
    }

    public void UnmuteMusic2()
    {
        _musicSource2.volume = 1f;
    }
}
