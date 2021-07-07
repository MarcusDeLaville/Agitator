using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Texts
{
    public Text LeafletsSmall;
    public Text LeafletsBig;
    public Text PaperSmall;
    public Text PaperBig;
    public Text Poster;
}

public class MaterialsText : MonoBehaviour
{
    [SerializeField] private Texts texts;

    public Texts Texts => texts;
}
