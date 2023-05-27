using System.Reflection;
using Microsoft.AspNetCore.Http.Metadata;
using MiniValidation;

namespace OmniRiskAPI.Filters;

public static class ValidationFiltersExtensions {
    public static TBuilder WithParameterValidation<TBuilder>(this TBuilder builder, params Type[] typesToValidate)
        where TBuilder : IEndpointConventionBuilder {
        builder.Add(eb => {
            var methodInfo = eb.Metadata.OfType<MethodInfo>().FirstOrDefault();

            if (methodInfo is null) {
                return;
            }

            List<int>? parameterIndexesToValidate = null;
            foreach (var p in methodInfo.GetParameters()) {
                if (typesToValidate.Contains(p.ParameterType)) {
                    parameterIndexesToValidate ??= new();
                    parameterIndexesToValidate.Add(p.Position);
                }
            }

            if (parameterIndexesToValidate is null) { 
                return;
            }

            eb.Metadata.Add(new ProducesResponseTypeMetadata(typeof(HttpValidationProblemDetails), 400,
                "application/problem+json"));

            eb.FilterFactories.Add((_, next) => {
                return efic => {
                    foreach (var index in parameterIndexesToValidate) {
                        if (efic.Arguments[index] is { } arg && !MiniValidator.TryValidate(arg, out var errors)) {
                            return new ValueTask<object?>(Results.ValidationProblem(errors));
                        }
                    }

                    return next(efic);
                };
            });
        });

        return builder;
    }

    private sealed class ProducesResponseTypeMetadata : IProducesResponseTypeMetadata {
        public ProducesResponseTypeMetadata(Type type, int statusCode, string contentType) {
            Type = type;
            StatusCode = statusCode;
            ContentTypes = new[] { contentType };
        }

        public Type Type { get; }
        public int StatusCode { get; }
        public IEnumerable<string> ContentTypes { get; }
    }
}