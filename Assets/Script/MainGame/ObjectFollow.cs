using System;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
   private enum Follow
   {
        mouse,
   }
   
    [SerializeField] private Follow follow;

    private void Update()
    {
        switch (follow)
        {
            case Follow.mouse:
                Vector3 mousePos = Input.mousePosition;
                transform.position = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+9);
                break;
        }
    }
}
