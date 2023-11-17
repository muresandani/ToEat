using System.Text.Json;
using Azure.AI.OpenAI;

public class InventoryItemFunctionDefinitions
{

    public static FunctionDefinition GetRemoveInventoryItemFunctionDefinition()
    {
        return new FunctionDefinition()
        {
            Name = "RemoveInventoryItem",
            Description = "Remove an item from a user's inventory",
            Parameters = BinaryData.FromObjectAsJson(
            new
            {
                Type = "object",
                Properties = new
                {
                    name = new
                    {
                        Type = "string",
                        Description = "The name of the product",
                    },
                    quantity = new
                    {
                        Type = "string",
                        Description = "The quantity of the given product",
                    },
                },
                Required = new[] { "name", "quantity" },
            },
            new JsonSerializerOptions() {  PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
        };
    }
}