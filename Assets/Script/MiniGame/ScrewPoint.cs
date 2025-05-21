using UnityEngine;

public class ScrewPoint : MonoBehaviour
{
    [SerializeField] private GameObject screwdriver;
    private bool isMouseOver = false;
    private bool isDragging = false;
    public int count;
    public bool Seccess = false;

    void Update()
    {
        if (isDragging && Input.GetMouseButton(0)) 
        {
            screwdriver.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            isDragging = false; 
            isMouseOver = false;
        }

        if (count == 5)
        {
            gameObject.SetActive(false);
            Seccess = true;
        }
    }

    private void OnMouseDown()
    {
        if (isMouseOver)
        {
            isDragging = true;
        }
    }

    private void OnMouseOver()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
    
    
}
