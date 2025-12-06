using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerMove : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A=-1£¨D=1£®X÷·£©
        float vertical = Input.GetAxisRaw("Vertical"); // S=-1£¨W=1£®Z÷·£©
        Vector3 moveDir=new Vector3(horizontal, vertical,0).normalized;
        transform.position += moveDir * moveSpeed;
    }
}
