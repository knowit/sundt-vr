using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HeveSenk : MonoBehaviour
{

    public GameObject bottom;
    public GameObject legs;
    public GameObject top;

    public bool up;
    public bool down;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (legs != null)
        {
            Debug.Log("update: legs " + legs.transform.localScale);
            Debug.Log("legs.position: " + legs.transform.position);
        }
        if (top != null)
        {
            Debug.Log("update: top " + top.transform.localScale);
            Debug.Log("top.position: " + top.transform.position);
        }

        if (top != null && legs != null)
        {
            if (up || Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("scaling up");
                legs.transform.localScale += new Vector3(0.0f, 0.2f, 0.0f);
            }
            else if (down || Input.GetKeyDown(KeyCode.D))
            {
                legs.transform.localScale -= new Vector3(0.0f, 0.2f, 0.0f);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                legs.transform.localScale = Vector3.one;
            }
            top.transform.localScale = Vector3.one;
        }
    }
}
