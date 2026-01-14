using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProximityBark : MonoBehaviour
{
    private AudioSource src;

    void Awake()
    {
        src = GetComponent<AudioSource>();
        src.playOnAwake = false;
        // Loop istiyorsan açýk kalsýn, istemiyorsan kapat:
        // src.loop = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!src.isPlaying) src.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        src.Stop();
    }
}
