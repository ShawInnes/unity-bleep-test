using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Transform loadingBar;

    [SerializeField]
    private Transform center;

    [Range(0, 1)]
    public float currentAmount;

    [Range(1, 50)]
    public float barThickness;

    public Color barColor;

    private void Update()
    {
        var bar = loadingBar.GetComponent<Image>();

        bar.fillAmount = currentAmount;
        bar.color = barColor;

        var rt = center.GetComponent<Image>().rectTransform;
        rt.offsetMax = new Vector2(-barThickness, -barThickness);
        rt.offsetMin = new Vector2(barThickness, barThickness);
    }
}
