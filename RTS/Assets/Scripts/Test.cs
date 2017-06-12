using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    int i = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
            this.i++;
        else
            this.i = 0;

        if (this.i != 0)
            Debug.Log(this.i);
	}
}
