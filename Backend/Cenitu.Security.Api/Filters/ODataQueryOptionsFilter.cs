using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ODataQueryOptionsFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        // 🌟 OData Query Parametrelerini Swagger içinde tanımlıyoruz:
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$filter",
            In = ParameterLocation.Query,
            Required = false,
            Schema = new OpenApiSchema { Type = "string" },
            Description = "Filter the results using OData syntax. Example: $filter=Price gt 100"
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$orderby",
            In = ParameterLocation.Query,
            Required = false,
            Schema = new OpenApiSchema { Type = "string" },
            Description = "Sort the results using OData syntax. Example: $orderby=Name desc"
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$top",
            In = ParameterLocation.Query,
            Required = false,
            Schema = new OpenApiSchema { Type = "integer" },
            Description = "Limit the number of results returned. Example: $top=10"
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$skip",
            In = ParameterLocation.Query,
            Required = false,
            Schema = new OpenApiSchema { Type = "integer" },
            Description = "Skip a certain number of results. Example: $skip=5"
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "$count",
            In = ParameterLocation.Query,
            Required = false,
            Schema = new OpenApiSchema { Type = "boolean" },
            Description = "Include a count of the total number of items in the response. Example: $count=true"
        });
    }
}
