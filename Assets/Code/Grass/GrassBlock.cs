using UnityEngine;

public class GrassBlock : MonoBehaviour
{
    private GrassPatchEncounterArea encounterArea;

    private void Awake()
    {
        encounterArea = GetComponentInParent<GrassPatchEncounterArea>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Player entered Grass");

        if (encounterArea != null)
        {
            encounterArea.RegisterPlayerStep(other.gameObject);
        }
        else
        {
            Debug.LogWarning($"{name} could not find a GrassPatchEncounterArea in its parents.");
        }

        if (other.TryGetComponent(out PlayerInteractor playerInteractor))
        {
            playerInteractor.isInTallGrass++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Player exited Grass");

        if (other.TryGetComponent(out PlayerInteractor playerInteractor))
        {
            playerInteractor.isInTallGrass = Mathf.Max(0, playerInteractor.isInTallGrass - 1);
        }
    }
}
