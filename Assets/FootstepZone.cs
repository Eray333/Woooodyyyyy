using UnityEngine;

public class FootstepZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var fs = other.GetComponent<FootstepsSimple>();
        if (fs) fs.SetWood();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var fs = other.GetComponent<FootstepsSimple>();
        if (fs) fs.SetGrass();
    }
}
