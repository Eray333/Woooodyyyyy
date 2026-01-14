using UnityEngine;

public class ForestAmbienceZone : MonoBehaviour
{
    public AudioSource source;
    public float targetVolume = 0.35f;
    public float fadeSpeed = 1.5f;

    bool inside = false;

    void Update()
    {
        float goal = inside ? targetVolume : 0f;
        source.volume = Mathf.MoveTowards(
            source.volume,
            goal,
            fadeSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inside = true;
        if (!source.isPlaying)
            source.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        inside = false;
    }
}
