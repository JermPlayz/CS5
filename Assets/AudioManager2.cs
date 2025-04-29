using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    [Header("-----Audio Source------")]
    [SerializeField] AudioBehaviour musicSource;

     [Header("-----Audio Clip------")]
     public AudioClip background;

   /* private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    */
}
