using System.Diagnostics.CodeAnalysis;

namespace RestApi.DTO
{
    [ExcludeFromCodeCoverage]
    public record TennisMatchDto(string[] Players, int[] Points);

}
