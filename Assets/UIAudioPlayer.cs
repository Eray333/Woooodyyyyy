using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;
    public AudioClip success;
    public AudioClip error;

    public void PlayHover() { if (hover) source.PlayOneShot(hover); }
    public void PlayClick() { if (click) source.PlayOneShot(click); }
    public void PlaySuccess() { if (success) source.PlayOneShot(success); }
    public void PlayError() { if (error) source.PlayOneShot(error); }
}
