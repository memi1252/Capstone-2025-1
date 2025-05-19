using UnityEngine;

public class Wire : MonoBehaviour
{
    public bool isCut = false;
    public bool isCount;
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.name == "CutObject")
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCut = true;
                Debug.Log("Wire Cut");
            }
        }
    }
}
