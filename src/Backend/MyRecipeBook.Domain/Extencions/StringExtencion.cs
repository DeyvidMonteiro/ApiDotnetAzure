using System.Diagnostics.CodeAnalysis;

namespace MyRecipeBook.Domain.Extencions;

public static class StringExtencion
{
    public static bool NotEmpty([NotNullWhen(true)]this string? value) => String.IsNullOrWhiteSpace(value).IsFalse();
}
