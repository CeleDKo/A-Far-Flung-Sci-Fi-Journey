using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuWorkshop : MonoBehaviour
{
    [SerializeField] private Product[] products;
    [SerializeField] private Balance balance;

    private void Start()
    {
        foreach (Product product in products)
        {
            product.BuyedLevel = PlayerPrefs.GetInt(product.Type + "Product", 1);
            product.Slider.value = product.BuyedLevel;

            if (product.Slider.value >= product.Slider.maxValue) product.PriceText.text = "MAX";
            else
            {
                product.Price = 10 * product.BuyedLevel;
                product.PriceText.text = product.Price.ToString();
                product.Button.onClick.AddListener(() => Buy(product));
            }
        }
    }

    public void Buy(Product product)
    {
        if (balance.CurrentBalance < product.Price || product.BuyedLevel >= product.Slider.maxValue) return;

        balance.Minus(product.Price);
        product.BuyedLevel++;
        product.Slider.value = product.BuyedLevel;
        PlayerPrefs.SetInt(product.Type + "Product", product.BuyedLevel);
        int neededValue = product.BuyedLevel;

        switch (product.Type)
        {
            case Type.Speed:
                neededValue = 3 + product.BuyedLevel;
                break;
            case Type.Fuel:
                neededValue = 50 + (product.BuyedLevel * 10);
                break;
        }

        PlayerPrefs.SetInt(product.Type.ToString(), neededValue);
        product.Price = 10 * product.BuyedLevel;
        if (product.Slider.value >= product.Slider.maxValue) product.PriceText.text = "MAX";
        else product.PriceText.text = product.Price.ToString();
    }

    [System.Serializable]
    public class Product
    {
        public TMP_Text PriceText;
        public int Price, BuyedLevel;
        public Type Type;
        public Button Button;
        public Slider Slider;
    }
    public enum Type
    {
        Speed,
        Fuel,
        Damage
    }
}