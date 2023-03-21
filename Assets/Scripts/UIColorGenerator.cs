using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIColorGenerator : MonoBehaviour
{
    [SerializeField] ColorManager _colorManager;
    [SerializeField] float _offset;
    [SerializeField] Image _imagePrefab;

    private void Start()
    {
        _colorManager = FindObjectOfType<ColorManager>();

        if (_colorManager && _imagePrefab)
        {
            var amount = _colorManager.GetColorAmount();
            float containerWidth = gameObject.transform.GetComponent<RectTransform>().rect.width;
            float totalOffset = (amount - 1) * _offset;
            float imageWidth = containerWidth;

            for (int i = amount - 1; i >= 0; i--)
            {
                Image newImage = Instantiate(_imagePrefab);

                RectTransform imageTransform = newImage.GetComponent<RectTransform>();
                imageTransform.SetParent(gameObject.transform);
                imageTransform.sizeDelta = new Vector2(imageWidth, imageWidth);
                imageTransform.anchoredPosition = new Vector2((i - (amount - 1)) * (imageWidth + totalOffset), 0f);

                newImage.color = _colorManager.GetColor(i);
            }
        }
    }

}
