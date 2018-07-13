using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using System.IO;
using System.Linq;
using System;
using UnityEngine.UI;

public class PlotData : MonoBehaviour
{
    public DotInfo dotObjectTemplate;
    public float dotScale = 1;
    public float maxPositiveCoordAxisValue = 65536f;
    public float maxPositivePlotAxisValue = 150f;
    public Vector3[] dataPoints = new Vector3[0];
    public Text loadingIndicator;

	void Start ()
    {
        HideLoadingIndicator();
	}
		

    public void OnLoadClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Load data point file", Directory.GetCurrentDirectory(), new[] { new ExtensionFilter("Text files", "txt") }, false);
        if (!paths.Any())
        {
            return;
        }
        var data = File.ReadAllText(paths[0]);
        var points = data.Split('\n')
            .Select(s => s.Trim())
            .Select(s => s.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries))
            .Where(a => a.Length == 4 && a[3] == "1")
            .Select(a =>
            {
                var v = Vector3.zero;
                int x, y, z;
                int.TryParse(a[0], out x);
                int.TryParse(a[1], out z);
                int.TryParse(a[2], out y);
                return new Vector3(x, y, z);
            })
            .ToArray();
        dataPoints = points;

        ShowLoadingIndicator();
        StartCoroutine(PlotPointsCoroutine());
    }

    public IEnumerator PlotPointsCoroutine()
    {
        yield return true;
        PlotPoints();
        yield return true;
        HideLoadingIndicator();
    }

    public void ClearPoints()
    {   
        var dots = GameObject.FindGameObjectsWithTag("DotObject");
        foreach(var dot in dots)
        {
            Destroy(dot);
        }
    }
        
    public void PlotPoints()
    {
        ClearPoints();
        
        for (int i = 0; i < dataPoints.Length; i++)
        {
            var point = dataPoints[i] / maxPositiveCoordAxisValue * maxPositivePlotAxisValue;
            var dot = Instantiate<DotInfo>(dotObjectTemplate, point, Quaternion.identity);
            dot.transform.localScale = new Vector3(dotScale, dotScale, dotScale);
            dot.name = "DotObject";
            dot.tag = "DotObject";
            dot.point = dataPoints[i];
        }        
    }

    public void ShowLoadingIndicator()
    {
        loadingIndicator.gameObject.SetActive(true);
    }

    public void HideLoadingIndicator()
    {
        loadingIndicator.gameObject.SetActive(false);
    }
}
