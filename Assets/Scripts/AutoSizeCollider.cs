using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSizeCollider : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<RectTransform>().rect.width - 20, GetComponent<RectTransform>().rect.height - 20);
    }
}
