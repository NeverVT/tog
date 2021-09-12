using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAddition : MonoBehaviour
{
    public int number = 0;
    private int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextMesh>().text = "+ " + number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 100)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + .02f, this.transform.position.z);
        else
            Destroy(this);
    }
}
