using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRayCast : NetworkBehaviour
{
    [SerializeField] private float rayRange = 3f;
    private RaycastHit hit;
    public IUseable selectedEntity;
    private void LateUpdate()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        DrawRay();
    }

    private void DrawRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.forward * rayRange, Color.green);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.distance <= rayRange && hit.collider.gameObject.GetComponent<IUseable>() != null)
            {
                selectedEntity = hit.collider.gameObject.GetComponent<IUseable>();
                CanvasManager.Instance.ChangeInteractText(selectedEntity.Tip());
                CanvasManager.Instance.SetActiveInteractText(true);
            }
            else
            {
                selectedEntity = null;
                CanvasManager.Instance.SetActiveInteractText(false);
            }
        }
    }

    
}
