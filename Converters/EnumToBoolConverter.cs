using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace WorkflowManager.Converters;

public class EnumToBoolConverter : IValueConverter
{
    // Converts Enum -> Boolean (For the View)
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || parameter == null) return false;

        // Check if the current Discriminator matches the parameter passed in XAML
        return value.ToString() == parameter.ToString();
    }

    // Unused for Visibility
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}