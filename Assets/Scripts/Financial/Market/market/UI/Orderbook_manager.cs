using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Orderbook_manager : MonoBehaviour
{
    public Text[] valueItems;
    public Text[] quantityItems;
    public Page_Manager page;
    float[] values;

    float rangeValue(float mid_price)
    {
        if (mid_price > 1000f)
        {
            return 100f;
        } else if (mid_price > 100f)
        {
            return 10f;
        } else if (mid_price > 10f)
        {
            return 5f;
        } else if (mid_price > 1f)
        {
            return 0.1f;
        } else if (mid_price > 0f)
        {
            return 0.01f;
        } else
        {
            return 0f;
        }
    }
    float[] generateValues()
    {
        float mid_price = page.ItemInStock.orderBook.marketPrice;
        float range = rangeValue(mid_price);

        float[] values_list = new float[18];
        float n_i = mid_price;
        for(int i = 0; i < 18; i++)
        {
            if (i == 0)
            {
                values_list[9] = n_i;
            } else if (i <= 9)
            {
                n_i -= range;
                values_list[9 - i] = n_i;
            } else if (i > 9)
            {
                if (i == 10)
                {
                    n_i = mid_price;
                }
                n_i += range;
                values_list[i] = n_i;
            }
        }
        return values_list;
    }

    private void Update()
    {
        int len = valueItems.Length;
        float[] values = generateValues();
        for (int i = 0; i < len; i++)
        {
            valueItems[i].text = values[i].ToString();
        }

        for (int i = 0; i < len; i++)
        {
            if (i < 9)
            {
                List<LimitOrder> buyorder = page.ItemInStock.orderBook.getBuyOrders();
                if (buyorder.Count == 0)
                {
                    continue;
                }

                int total_quantity = 0;
                foreach(LimitOrder order in buyorder)
                {
                    if (order.Price == values[i])
                    {
                        total_quantity += order.Quantity;
                    }
                }
                quantityItems[i].text = total_quantity.ToString();
            }
            else
            {
                List<LimitOrder> sellorder = page.ItemInStock.orderBook.getSellOrders();
                if (sellorder.Count == 0)
                {
                    continue;
                }
                int total_quantity = 0;
                foreach (LimitOrder order in sellorder)
                {
                    if (order.Price == values[i])
                    {
                        total_quantity += order.Quantity;
                    }
                }
                quantityItems[i].text = total_quantity.ToString();
            }
        }
    }
}
