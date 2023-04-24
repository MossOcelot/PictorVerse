using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiLineChartController : MonoBehaviour
{
    public RectTransform chartContainer;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    public RectTransform lineTemplate;

    [System.Serializable]
    public class dataDetails
    {
        public int index;
        public List<float> data;
    }
    public List<dataDetails> Datas;
    public List<Color> lineColors;
    public List<string> labels;


    public void CreateChart()
    {
        // Clear existing chart
        foreach (Transform child in chartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Get chart dimensions
        float chartHeight = chartContainer.rect.height * 0.7f;
        float chartWidth = chartContainer.rect.width * 0.75f;

        // Get label dimensions
        float labelXWidth = labelTemplateX.rect.width * 0.4f;
        float labelYWidth = labelTemplateY.rect.width * 0.4f;

        // Get line dimensions
        float maxValue = GetMaxValue();
        float chartYScale = chartHeight / maxValue;

        // Create X labels
        for (int i = 0; i < Datas[0].data.Count; i++)
        {
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(chartContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(-120 + (labelYWidth + (i * chartWidth / (Datas[0].data.Count - 1))), -80f);
            labelX.GetComponent<Text>().text = labels[i];
        }

        // Create Y labels
        int numLabels = 5;
        for (int i = 0; i <= numLabels; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(chartContainer, false);
            labelY.gameObject.SetActive(true);
            float labelYPosition = chartHeight * i / numLabels;
            labelY.anchoredPosition = new Vector2(- 120 + (-labelYWidth / 2f), -70 + labelYPosition);
            labelY.GetComponent<Text>().text = ToShortString(Mathf.RoundToInt(maxValue * i / numLabels));
        }

        // Create lines for data
        for (int i = 0; i < Datas.Count; i++)
        {
            RectTransform line = Instantiate(lineTemplate);
            line.SetParent(chartContainer, false);
            line.gameObject.SetActive(true);
            line.GetComponent<UILineRenderer>().color = lineColors[i];
            Vector2[] points = GetPoints(Datas[i].data, chartWidth * 0.09f, chartHeight, labelYWidth, chartYScale * 0.08f);
            line.GetComponent<UILineRenderer>().points = points;

        }
    }

    // -234.2 4.7549

    private Vector2[] GetPoints(List<float> values, float chartWidth, float chartHeight, float labelYWidth, float chartYScale)
    {
        Vector2[] points = new Vector2[values.Count];

        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = labelYWidth + (i * chartWidth / (values.Count - 1));
            float yPosition = values[i] * chartYScale;
            points[i] = new Vector2(xPosition - 19, yPosition - 5f);
        }

        return points;
    }

    private float GetMaxValue()
    {
        float maxValue = Mathf.NegativeInfinity;

        for (int i = 0; i < Datas.Count; i++)
        {
            float value = Mathf.Max(Datas[i].data.ToArray());

            if (value > maxValue)
            {
                maxValue = value;
            }
        }

        return maxValue;
    }

    public static string ToShortString(int value)
    {
        if (value >= 1000000000)
            return (value / 1000000000D).ToString("0.#") + "B";
        if (value >= 1000000)
            return (value / 1000000D).ToString("0.#") + "M";
        if (value >= 1000)
            return (value / 1000D).ToString("0.#") + "K";
        return value.ToString("#,0");
    }
}


