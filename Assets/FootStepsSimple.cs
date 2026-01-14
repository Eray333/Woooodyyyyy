using UnityEngine;

public class FootstepsSimple : MonoBehaviour
{
    public AudioSource source;

    [Header("Footstep Sets")]
    public AudioClip[] grassSteps;
    public AudioClip[] woodSteps;

    [Header("Settings")]
    public float stepInterval = 0.45f;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    float timer;
    AudioClip[] currentSteps;

    void Start()
    {
        if (!source) source = GetComponent<AudioSource>();
        currentSteps = grassSteps; // default: dýþarý
    }

    void Update()
    {
        bool moving =
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f;

        if (!moving)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;
        if (timer >= stepInterval)
        {
            timer = 0f;

            if (currentSteps == null || currentSteps.Length == 0) return;

            source.pitch = Random.Range(minPitch, maxPitch);
            source.PlayOneShot(currentSteps[Random.Range(0, currentSteps.Length)]);
        }
    }

    public void SetGrass() => currentSteps = grassSteps;
    public void SetWood() => currentSteps = woodSteps;
}
