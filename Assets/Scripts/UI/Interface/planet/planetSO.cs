using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class planetSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite planetImage { get; set; }
    public int ID => GetInstanceID();
    [field: SerializeField]
    public Sprite planetSymbol { get; set; }
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string location { get; set; }
    [field: SerializeField]
    public string rank { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string uniqueness { get; set; }
    [field: SerializeField]
    [field: TextArea]
    public string Advantage { get; set; }
    [field: SerializeField]
    [field: TextArea]
    public string Disadvantage { get; set; }
    [field: SerializeField]
    public Sprite resource { get; set; }

}
