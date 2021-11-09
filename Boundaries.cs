using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour{

    private Vector2 screenBounds; 


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log(screenBounds); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
    //    viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
    //    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);

        viewPos.x = Mathf.Clamp(viewPos.x, -2, 2);
        viewPos.y = Mathf.Clamp(viewPos.y, -2, 4);
        transform.position = viewPos; 
    }
}
