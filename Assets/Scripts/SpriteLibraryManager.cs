using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using System.Linq;
[RequireComponent(typeof(SpriteResolver))]
public class SpriteLibraryManager : MonoBehaviour
{
    private SpriteResolver spriteResolver;
    private SpriteLibrary spriteLibrary;
    private void Awake()
    {
        spriteResolver = GetComponent<SpriteResolver>();
        spriteLibrary = GetComponent<SpriteLibrary>();
    }
    private void Start()
    {
        string category = spriteResolver.GetCategory();
        var labels = spriteLibrary.spriteLibraryAsset.GetCategoryLabelNames(category).ToArray();
        string randomLabel = labels[Random.Range(0, labels.Length)];

        spriteResolver.SetCategoryAndLabel(category, randomLabel);
    }
}
