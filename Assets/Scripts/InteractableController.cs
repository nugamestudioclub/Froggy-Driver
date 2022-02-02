using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableController : MonoBehaviour, IPointerDownHandler
{
    public CarController car;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
