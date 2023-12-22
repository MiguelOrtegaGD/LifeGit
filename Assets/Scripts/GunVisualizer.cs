using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVisualizer : MonoBehaviour
{
    Vector3 _offset;
    Vector3 _rotation;
    GameObject _gunObj;

    public GameObject GunObj { get => _gunObj; set => _gunObj = value; }
    public Vector3 Offset { get => _offset; set => _offset = value; }
    public Vector3 Rotation { get => _rotation; set => _rotation = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
