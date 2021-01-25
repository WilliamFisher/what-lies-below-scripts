using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    private Camera _myCam = null;

    private bool showingText = false;
    private bool lookingAtInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        _myCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _myCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 6.0f))
        {
            if(hit.transform.tag == "Interactable")
            {
                showingText = true;
                lookingAtInteractable = true;
                IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
                ActionTextHandler.Instance.SetText(interactable.GetInteractText());
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(gameObject);
                }
                
            }
            else
            {
                lookingAtInteractable = false;
            }
        }

        if(showingText && !lookingAtInteractable)
        {
            ActionTextHandler.Instance.Clear();
            showingText = false;
        }
    }
}
