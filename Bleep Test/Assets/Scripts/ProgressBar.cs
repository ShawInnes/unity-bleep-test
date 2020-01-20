using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Transform loadingBar;

    [SerializeField]
    private Transform center;

    public bool useSmoothing = false;
    public float smoothingFactor = 3;

    [Range(0, 1)]
    public float currentAmount = 0f;

    [Range(1, 50)]
    public float barThickness;

    public Color barColor;

    private void Update()
    {
        var bar = loadingBar.GetComponent<Image>();

        bar.fillAmount = useSmoothing ? Mathf.Lerp(bar.fillAmount, currentAmount, Time.deltaTime * smoothingFactor) : currentAmount;
        bar.color = barColor;

        var rt = center.GetComponent<Image>().rectTransform;
        rt.offsetMax = new Vector2(-barThickness, -barThickness);
        rt.offsetMin = new Vector2(barThickness, barThickness);
    }
}
