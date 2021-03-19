using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour
{
    // Start is called before the first frame update

    //imagedrag
    public RawImage image;
    public GameObject Border;
    private Vector3 imageCenter;
    private float speed = 8f;
    private float leftBoundary;
    private float rightBoundary;
    private float upperBoundary;
    private float lowerBoundary;

    private Vector3 delta;

    void Start()
    {
        UpdateImage(image);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            delta = image.transform.position - Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {

            Vector3 newPos = Input.mousePosition + delta;
            image.transform.position = newPos;

            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x > rightBoundary || mousePos.x < leftBoundary || mousePos.y > upperBoundary || mousePos.y < lowerBoundary)
            {
                Debug.Log("out mouse");
            }
            if (image.transform.position.x > rightBoundary || image.transform.position.x < leftBoundary || image.transform.position.y > upperBoundary || image.transform.position.y < lowerBoundary)
            {
                Debug.Log("out image");
            }
        }
        else
        {
            float newX = Mathf.Lerp(image.transform.position.x, imageCenter.x, Time.deltaTime * speed);
            float newY = Mathf.Lerp(image.transform.position.y, imageCenter.y, Time.deltaTime * speed);
            image.transform.position = new Vector3(newX, newY, imageCenter.z);
            if (image.transform.position.x < imageCenter.x + 2 && image.transform.position.x > imageCenter.x - 2)
            {
                if (image.transform.position.y < imageCenter.y + 2 && image.transform.position.y > imageCenter.y - 2)
                {
                    image.transform.position = new Vector3(imageCenter.x, imageCenter.y, imageCenter.z);
                }
            }
        }
    }

    public void UpdateImage(RawImage image)
    {
        this.image = image;
        imageCenter = image.transform.position;
        RectTransform rectTransform = Border.GetComponent<RectTransform>();
        leftBoundary = rectTransform.rect.position.x - rectTransform.rect.width /2;
        rightBoundary = rectTransform.rect.position.x + rectTransform.rect.width / 2;

        upperBoundary = rectTransform.rect.position.y + rectTransform.rect.height / 2;
        lowerBoundary = rectTransform.rect.position.y - rectTransform.rect.height / 2;

    }
}
