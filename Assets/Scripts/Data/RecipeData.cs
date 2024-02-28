using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
    {
        [JsonProperty("name")]
        public string name ;

        [JsonProperty("servingSize")]
        public ServingSize servingSize ;
    }
[System.Serializable]
public class Nutrients
    {
        [JsonProperty("caloriesKCal")]
        public double caloriesKCal ;

       
        [JsonProperty("totalCarbs")]
        public double totalCarbs ;

       

        [JsonProperty("diabetesCarbs")]
        public double diabetesCarbs ;

      
        [JsonProperty("sugar")]
        public double sugar ;

      
        [JsonProperty("protein")]
        public double protein ;

        [JsonProperty("fat")]
        public double fat ;

      
        [JsonProperty("potassium")]
        public double potassium ;

        [JsonProperty("magnesium")]
        public double magnesium ;

        [JsonProperty("calcium")]
        public double calcium ;

        [JsonProperty("iron")]
        public double iron ;

        [JsonProperty("zinc")]
        public double zinc ;

        [JsonProperty("copper")]
        public double copper ;

        [JsonProperty("phosphorus")]
        public double phosphorus ;

        [JsonProperty("sodium")]
        public double sodium ;

        [JsonProperty("selenium")]
        public double selenium ;

        [JsonProperty("folate")]
        public double folate ;

        [JsonProperty("choline")]
        public double choline ;

        [JsonProperty("alcohol")]
        public double alcohol ;

        [JsonProperty("caffeine")]
        public double caffeine ;

        [JsonProperty("gluten")]
        public double gluten ;

        [JsonProperty("manganese")]
        public double manganese ;

    }

[System.Serializable]
    public class RecipeData
    {
        [JsonProperty("id")]
        public string id ;

    [TextArea(1,2)]
    [JsonProperty("name")]
        public string name ;

        [JsonProperty("tags")]
        public List<string> tags ;
    [TextArea(2, 10)]
    [JsonProperty("description")]
        public string description ;

        [JsonProperty("prepareTime")]
        public double prepareTime ;

        [JsonProperty("cookTime")]
        public double cookTime ;

        [JsonProperty("ingredients")]
        public List<Ingredient> ingredients ;

        [JsonProperty("steps")]
        public List<string> steps ;

        [JsonProperty("servings")]
        public double servings ;

        [JsonProperty("servingSizes")]
        public List<ServingSize> servingSizes ;

        [JsonProperty("nutrients")]
        public Nutrients nutrients ;

        [JsonProperty("image")]
        public string image ;
    }

[System.Serializable]
public class ServingSize
    {
        [JsonProperty("units")]
        public string units ;

        [JsonProperty("desc")]
        public string desc ;

        [JsonProperty("qty")]
        public double qty ;

        [JsonProperty("grams")]
        public double grams ;

        [JsonProperty("scale")]
        public double scale ;
    }

[System.Serializable]
public class ServingSize2
    {
        [JsonProperty("scale")]
        public double scale ;

        [JsonProperty("qty")]
        public double qty ;

        [JsonProperty("grams")]
        public double grams ;

        [JsonProperty("units")]
        public string units ;

        [JsonProperty("originalWeight")]
        public double originalWeight ;

       
    }





