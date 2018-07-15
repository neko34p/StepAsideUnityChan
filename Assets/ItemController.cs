using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    private GameObject unitychan;

    // Use this for initialization
    void Start () {
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update () {
        // Unityちゃんよりも後ろになったら破棄
        if (this.transform.position.z < this.unitychan.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
