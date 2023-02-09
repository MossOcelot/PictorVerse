using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeRate : MonoBehaviour
{
    private Dictionary<int, Dictionary<int, float>> databaseExchangeRate =
        new Dictionary<int, Dictionary<int, float>>
        {
            { 0, new Dictionary<int, float> {
                    {0, 1},
                    {1, 0.95f},
                    {2, 0.5f},
                    {3, 5f },
                    {4, 35f},
                    {5, 0.00048f },
                }
            },
            { 1, new Dictionary<int, float> {
                    {0, 1.05f},
                    {1, 1},
                    {2, 0.525f},
                    {3, 5.25f },
                    {4, 36.75f},
                    {5, 0.0005f },
                }
            },
            { 2, new Dictionary<int, float> {
                    {0, 2f},
                    {1, 1.9f},
                    {2, 1},
                    {3, 10f },
                    {4, 70f},
                    {5, 0.00096f },
                }
            },
            { 3, new Dictionary<int, float> {
                    {0, 0.2f},
                    {1, 0.19f},
                    {2, 0.1f },
                    {3, 1},
                    {4, 7f},
                    {5, 0.000048f },
                }
            },
            { 4, new Dictionary<int, float> {
                    {0, 0.0285f},
                    {1, 0.027f},
                    {2, 0.014f },
                    {3, 0.1425f},
                    {4, 1},
                    {5, 0.000013f },
                }
            },
            {5, new Dictionary<int, float> {
                    {0, 2083.33f},
                    {1, 2000f},
                    {2, 1041.67f },
                    {3, 20833.33f},
                    {4, 76923.08f },
                    {5, 1 }
                }
            }
        };
    public float getExchangeRate(int fromCurrency, int toCurrency)
    {
        float exchangeRate = databaseExchangeRate[fromCurrency][toCurrency];

        return exchangeRate;
    }

    
}
