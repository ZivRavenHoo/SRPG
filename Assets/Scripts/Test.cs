using System.Collections;
using System.Collections.Generic;
using ImpulseUtility;
using UnityEngine;
using LitJson;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GridMapData gridMapData = new GridMapData(new Size(18, 10));
        string json = JsonMapper.ToJson(gridMapData);
        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
