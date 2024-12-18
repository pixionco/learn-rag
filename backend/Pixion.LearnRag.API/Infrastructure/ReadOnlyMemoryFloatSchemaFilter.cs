using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pixion.LearnRag.API.Infrastructure;

public class ReadOnlyMemoryFloatSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ReadOnlyMemory<float>))
        {
            // Replace the schema with an array of floats
            schema.Type = "array";
            schema.Items = new OpenApiSchema { Type = "number", Format = "float" };
        }
    }
}