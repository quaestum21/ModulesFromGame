using UnityEngine;

[RequireComponent(typeof(AudioSource))] 
public class AndroidSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _jumpClips;
    [SerializeField] private AudioClip[] _attackClips;
    [SerializeField] private AudioClip[] _deathClips;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }
    public void PlayClipOneShot(AndroidClipType type)
    {
        _audioSource.Stop();
        AudioClip clip = null;
        switch (type)
        {
            case AndroidClipType.Attack:
                clip = _attackClips[Random.Range(0, _attackClips.Length)];
                break;
            case AndroidClipType.Death:
                clip = _deathClips[Random.Range(0, _deathClips.Length)];
                break;
            case AndroidClipType.Jump:
                clip = _jumpClips[Random.Range(0, _jumpClips.Length)];
                break;
        }
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
public enum AndroidClipType
{
    Attack,
    Death,
    Jump
}
