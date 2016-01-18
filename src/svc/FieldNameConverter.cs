namespace BryanPorter.SlackCmd
{
    using System;
    using Nancy.ModelBinding;

    /// <summary>
    /// Converts field names to Pascal case and removes underscores.
    /// </summary>
    /// <remarks>
    /// This converter replaces the default Nancy field converter, and allows model binding to fields with names
    /// that don't match the typical Pascal casing found in .NET classes.
    /// </remarks>
    public class FieldNameConverter
        : IFieldNameConverter
    {
        public string Convert(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                return fieldName;

            var first = fieldName.Substring(0, 1);
            var capitalFirst = first.ToUpperInvariant();

            if (fieldName.IndexOf("_", StringComparison.OrdinalIgnoreCase) != -1)
                fieldName = fieldName.Replace("_", "");

            return first.Equals(capitalFirst) ? fieldName : $"{capitalFirst}{fieldName.Substring(1)}";
        }
    }
}
