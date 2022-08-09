using System;
using System.Collections.Generic;

namespace BootstrapHtmlHelpersPackage
{
    public static class AttributeHelper
    {
        public static void SetInputTypeAttributesByType(this IDictionary<string, object> attributes, InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Default:
                    attributes.AddType("text");
                    break;

                case InputType.Button:
                    throw new NotImplementedException(TypeExceptionMessage("Button"));

                case InputType.Checkbox:
                    attributes.AddType("checkbox");
                    break;

                case InputType.Color:
                    attributes.AddType("color");
                    break;

                case InputType.Date:
                    attributes.AddType("date");
                    break;

                case InputType.DateTimeLocal:
                    throw new NotImplementedException(TypeExceptionMessage("DateTimeLocal", "Date"));

                case InputType.Email:
                    attributes.AddType("email");
                    break;

                case InputType.File:
                    attributes.AddType("file");
                    break;

                case InputType.Hidden:
                    throw new InvalidOperationException(TypeExceptionMessage("Hidden", "@Html.HiddenFor()"));

                case InputType.Image:
                    attributes.AddType("image");
                    break;

                case InputType.Month:
                    throw new NotImplementedException(TypeExceptionMessage("Month", "Date"));

                case InputType.Number:
                    attributes.AddType("number");
                    break;

                case InputType.Password:
                    attributes.AddType("password");
                    break;

                case InputType.Radio:
                    attributes.AddType("radio");
                    break;

                case InputType.Range:
                    attributes.AddType("range");
                    break;

                case InputType.Reset:
                    attributes.AddType("reset");
                    throw new InvalidOperationException(TypeExceptionMessage("Reset"));

                case InputType.Search:
                    attributes.AddType("search");
                    break;

                case InputType.Submit:
                    attributes.AddType("submit");
                    throw new InvalidOperationException(TypeExceptionMessage("Submit", "@Html.Submit()"));

                case InputType.Tel:
                    attributes.AddType("tel");
                    break;

                case InputType.Text:
                    attributes.AddType("text");
                    break;

                case InputType.Time:
                    throw new NotImplementedException(TypeExceptionMessage("Time", "Date"));

                case InputType.Url:
                    attributes.AddType("url");
                    break;

                case InputType.Week:
                    throw new NotImplementedException(TypeExceptionMessage("Week", "Date"));

                default:
                    // Should never occur.
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }

        public static void SetInputClassAttributesByType(this IDictionary<string, object> attributes, InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Default:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Button:
                    throw new NotImplementedException(TypeExceptionMessage("Button"));

                case InputType.Checkbox:
                    attributes.AddClass(ClassHelper.FormCheckInputStyleClass);
                    break;

                case InputType.Color:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Date:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.DateTimeLocal:
                    throw new NotImplementedException(TypeExceptionMessage("DateTimeLocal", "Date"));

                case InputType.Email:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.File:
                    attributes.AddClass(ClassHelper.FormControlFileStyleClass);
                    break;

                case InputType.Hidden:
                    throw new InvalidOperationException(TypeExceptionMessage("Hidden", "@Html.HiddenFor()"));

                case InputType.Image:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Month:
                    throw new NotImplementedException(TypeExceptionMessage("Month", "Date"));

                case InputType.Number:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Password:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Radio:
                    attributes.AddClass(ClassHelper.FormCheckInputStyleClass);
                    break;

                case InputType.Range:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Reset:
                    throw new InvalidOperationException(TypeExceptionMessage("Reset"));

                case InputType.Search:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Submit:
                    throw new InvalidOperationException(TypeExceptionMessage("Submit", "@Html.Submit()"));

                case InputType.Tel:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Text:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Time:
                    throw new NotImplementedException(TypeExceptionMessage("Time", "Date"));

                case InputType.Url:
                    attributes.AddClass(ClassHelper.FormControlStyleClass);
                    break;

                case InputType.Week:
                    throw new NotImplementedException(TypeExceptionMessage("Week", "Date"));

                default:
                    // Should never occur.
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }

        public static void SetLabelClassAttributesByType(this IDictionary<string, object> attributes, InputType inputType)
        {
            // If the input type is a checkbox or radio button, the label uses ClassHelper.FormCheckLabelStyleClass.
            if (inputType == InputType.Checkbox || inputType == InputType.Radio)
            {
                attributes.AddClass(ClassHelper.FormCheckLabelStyleClass);
            }
            else
            {
                attributes.AddClass(ClassHelper.FormLabelStyleClass);
            }
        }

        public static void AddType(this IDictionary<string, object> attributes, string type)
        {
            attributes.Add("type", type);
        }

        public static void AddClass(this IDictionary<string, object> attributes, string @class)
        {
            attributes.Add("class", @class);
        }

        public static string TypeExceptionMessage(string type, string substitute = null)
        {
            var typeMessage =
                $"{type} is an input type that is not yet implemented or should not be used with @Html.ClassHelper.FormGroup() or @Html.ClassHelper.FormGroupFor().";

            var substituteMessage = string.Empty;

            if (substitute != null)
                substituteMessage = $"Please use {substitute} instead.";

            var message = typeMessage + " " + substituteMessage;

            return message;
        }
    }
}
