using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    public RectTransform MyArrow { get => arrow; set => arrow = value; }

    //----------------------------------------------------------------------------//
    [SerializeField] private TextMeshProUGUI f, g, h, p;

    public TextMeshProUGUI F { get => f; set => f = value; }
    public TextMeshProUGUI G { get => g; set => g = value; }
    public TextMeshProUGUI H { get => h; set => h = value; }
    public TextMeshProUGUI P { get => p; set => p = value; }   
}
