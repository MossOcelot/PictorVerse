using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MultiBarChart : MonoBehaviour
{
    public List<float> values1;
    public List<float> values2;
    public List<string> labels;

    public RectTransform chartContainer;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    public RectTransform barTemplate1;
    public RectTransform barTemplate2;

    public void CreateChart()
    {
        // Clear existing chart
        foreach (Transform child in chartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Get chart dimensions
        float chartHeight = chartContainer.rect.height * 0.9f;
        float chartWidth = chartContainer.rect.width * 0.7f;

        // Get label dimensions
        float labelXWidth = labelTemplateX.rect.width;
        float labelYWidth = labelTemplateY.rect.width * 0.9f;

        // Get bar dimensions
        float barWidth = (chartWidth - labelYWidth) / (values1.Count * 2 - 1);
        float maxValue = Mathf.Max(Mathf.Max(values1.ToArray()), Mathf.Max(values2.ToArray()));
        float chartYScale = chartHeight / maxValue;

        // Create X labels
        for (int i = 0; i < values1.Count; i++)
        {
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(chartContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(-250 + (labelYWidth + (i * 2) * barWidth), -120f);
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
            labelY.anchoredPosition = new Vector2(-250 + (-labelYWidth / 2f), -100 + labelYPosition);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(maxValue * i / numLabels).ToString();
        }

        // Create bars for values1
        for (int i = 0; i < values1.Count; i++)
        {
            RectTransform bar = Instantiate(barTemplate1);
            bar.SetParent(chartContainer, false);
            bar.gameObject.SetActive(true);
            float barHeight = values1[i] * chartYScale;
            bar.sizeDelta = new Vector2(barWidth, barHeight);
            bar.anchoredPosition = new Vector2( -250 + (labelYWidth + (i * 2) * barWidth), -100 + (barHeight / 2f));
        }

        // Create bars for values2
        for (int i = 0; i < values2.Count; i++)
        {
            RectTransform bar = Instantiate(barTemplate2);
            bar.SetParent(chartContainer, false);
            bar.gameObject.SetActive(true);
            float barHeight = values2[i] * chartYScale;
            bar.sizeDelta = new Vector2(barWidth, barHeight);
            bar.anchoredPosition = new Vector2(-250 + (labelYWidth + ((i * 2) + 1) * barWidth), -100 + (barHeight / 2f));
        }
    }
}
