using UnityEngine;

public class GrassTriggerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    } 

    private void OnTriggerEnter()
    {
        Debug.Log("Entered Grass Trigger");
    }
    private void OnTriggerExit()
    {
        Debug.Log("Exited Grass Trigger");
    }
}