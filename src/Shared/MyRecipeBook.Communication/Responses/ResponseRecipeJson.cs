﻿using MyRecipeBook.Communication.Enums;

namespace MyRecipeBook.Communication.Responses;

public class ResponseRecipeJson
{
    public string Id { get; set; } = string.Empty;
    public string Title {  get; set; } = string.Empty;
    public IList<ResponseIngredientJson> Ingredients { get; set; } = [];
    public IList<ResponseInstructionJson> Instructions {  get; set; } = [];
    public IList<DishType> DishTypes { get; set; } = [];
    public CookingTime? cookingTime { get; set; }
    public Difficulty? difficulty { get; set; }
}
