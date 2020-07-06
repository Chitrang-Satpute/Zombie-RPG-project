using UnityEngine;
using UnityEngine.UI;

public class GunCrosshair : MonoBehaviour
{

    [SerializeField] float resetSize;
    [SerializeField] float maxSixe;
    [SerializeField] float speed;

    RectTransform rect;
    float currentSize;



    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(isMoving)
        {
            currentSize = Mathf.Lerp(currentSize, maxSixe, Time.deltaTime * speed);
        }
        else
        {
            currentSize = Mathf.Lerp(currentSize, resetSize, Time.deltaTime * speed);
        }
        rect.sizeDelta = new Vector2(currentSize, currentSize);
    }

    bool isMoving
    {
        get
        {
            if (Input.GetAxis("Horizontal") != 0 
                || 
                Input.GetAxis("Vertical") != 0 
                || 
                Input.GetAxis("Mouse X") != 0 
                || 
                Input.GetAxis("Mouse Y") !=0)
                return true;
            else
                return false;
        }
        
    }

}
