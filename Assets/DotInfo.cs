using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotInfo : MonoBehaviour
{
    public Vector3 point;
    public float DistanceToOrigin
    {
        get
        {
            return point.magnitude;
        }
    }

    private void OnMouseEnter()
    {
        var label = GameObject.FindGameObjectWithTag("DistanceLabel").GetComponent<Text>();
        label.text = string.Format("Point: {0} Distance: {1:0.0}", point, DistanceToOrigin);
    }
}
