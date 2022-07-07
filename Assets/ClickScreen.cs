using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScreen : MonoBehaviour
{
    private GameObject click;
    void Start()
    {
        click = Resources.Load<GameObject>("ClickInstan");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            GameObject ins =  Instantiate(click, transform.position, transform.rotation);
            ins.transform.position = pos;
            Destroy(ins, 1f);
        }
    }
}
