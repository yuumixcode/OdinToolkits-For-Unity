using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.LearnArchive.EditorExtensions.IMGUI._7_使用IMGUI扩展编辑器._2_自定义PropertyDrawer
{
    public enum IngredientUnit
    {
        Spoon,
        Cup,
        Bowl,
        Piece
    }

    // Custom serializable class
    [Serializable]
    public class Ingredient
    {
        public string name;
        public int amount = 1;
        public IngredientUnit unit;
    }

    public class Recipe : MonoBehaviour
    {
        [UnityDocRange(1, 10)] public int myInt;
        [UnityDocRange(0.1f, 5.0f)] public float myFloat;
        public Ingredient potionResult;
        public Ingredient[] potionIngredients;
    }
}