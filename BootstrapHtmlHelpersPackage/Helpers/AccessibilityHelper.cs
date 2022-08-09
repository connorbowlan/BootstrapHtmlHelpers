using System;
using System.Collections.Generic;
using BootstrapHtmlHelpersPackage.Enums;

namespace BootstrapHtmlHelpersPackage.Helpers
{
    /// <summary>
    /// This class adds attributes to our HTML helpers to provide support for screen readers
    /// or any other accessibility features.
    /// See https://www.w3.org/WAI/tutorials/ for documentation on how these methods were developed.
    /// </summary>
    public static class AccessibilityHelper
    {
        public static void SetAccessibilityAttributesByType(this IDictionary<string, object> attributes, string id, AccessibilityType accessibilityType)
        {
            switch (accessibilityType)
            {
                case AccessibilityType.Label:
                    attributes.Add("aria-label", id);
                    break;
                case AccessibilityType.FormControl:
                    attributes.Add("aria-labelledby", id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(accessibilityType), accessibilityType, null);
            }
        }
    }
}