using System.Text.Json;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pixion.LearnRag.API.Infrastructure.SwashbuckleFilters;

public class CamelCaseParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters != null)
            foreach (var parameter in operation.Parameters)
                // Convert parameter names to camelCase
                parameter.Name = JsonNamingPolicy.CamelCase.ConvertName(parameter.Name);
    }
}