using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BootstrapHtmlHelpersPackage
{
    public static class FormHelper
    {
        #region Constants

        // HTML and CSS classes used to construct a standard form group, or a form group for a check box or radio button.
        private const string FormGroupOpenHtml = "<div class=\"form-group\">";
        private const string FormCheckOpenHtml = "<div class=\"form-group form-check\">";
        private const string FormGroupCloseHtml = "</div>";

        // CSS classes used to style a standard input label, or an input label for a check box or radio button.
        private const string FormLabelStyleClass = "form-label";
        private const string FormCheckLabelStyleClass = "form-check-label";

        // CSS classes used to style a standard input field, or any other type potentially specified.
        private const string FormControlStyleClass = "form-control";
        private const string FormCheckInputStyleClass = "form-check-input";
        private const string FormControlFileStyleClass = "form-control-file";

        // CSS classes used to style validation results via jQuery Unobtrusive Validation.
        private const string ValidationMessageStyleClass = "text-danger";

        // CSS classes used to style buttons.
        private const string DefaultButtonStyleClass = "btn btn-primary";

        #endregion

        #region Form Groups

        public static MvcHtmlString CreateFormGroup<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            InputType inputType,
            string format = null,
            object htmlAttributes = null)
        {
            var label = CreateLabel(htmlHelper, expression, inputType);
            var inputField = CreateInputField(htmlHelper, expression, inputType, format, htmlAttributes);
            var validationMessage = CreateValidationMessage(htmlHelper, expression);

            string content;

            // If the input type is a checkbox or a radio button, the label comes second.
            if (inputType == InputType.Checkbox || inputType == InputType.Radio)
            {
                content = inputField.ToString() + label + validationMessage;
            }
            else
            {
                content = label + inputField.ToString() + validationMessage;
            }

            var formGroup = WrapFormGroup(inputType, content);

            return MvcHtmlString.Create(formGroup);
        }

        public static MvcHtmlString CreateFormGroup<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            var label = CreateLabel(htmlHelper, expression, InputType.Default);
            var dropdownField = CreateDropDownField(htmlHelper, expression, selectList, optionLabel, htmlAttributes);
            var validationMessage = CreateValidationMessage(htmlHelper, expression);

            var content = label + dropdownField.ToString() + validationMessage;

            var formGroup = WrapFormGroup(InputType.Default, content);

            return MvcHtmlString.Create(formGroup);
        }


        #region Label, Input Field, Dropdown Field (SelectList/Enum), and Validation Message

        private static MvcHtmlString CreateLabel<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, InputType inputType)
        {
            var attributes = new Dictionary<string, object>();

            attributes.SetLabelClassAttributesByType(inputType);

            return htmlHelper.LabelFor(expression, attributes);
        }

        private static MvcHtmlString CreateInputField<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, InputType inputType, string format = null,
            object htmlAttributes = null)
        {
            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            // Check for custom HTML attributes passed in.
            if (htmlAttributes != null)
            {
                // Custom HTMl attributes has specified a type via htmlAttributes parameter, throw an error.
                if (!attributes.ContainsKey("type"))
                {
                    attributes.SetInputTypeAttributesByType(inputType);
                }
                else
                {
                    throw new InvalidOperationException(
                        "\"Type\" is not a valid argument for htmlAttributes. Please specify a type using the InputType enum.");
                }

                // Check HTML attributes has specified a class, use them and not our Bootstrap classes.
                if (!attributes.ContainsKey("class"))
                {
                    attributes.SetInputClassAttributesByType(inputType);
                }

                return htmlHelper.TextBoxFor(expression, format, attributes);
            }

            // No htmlAttributes specified, use defaults.
            attributes.SetInputTypeAttributesByType(inputType);
            attributes.SetInputClassAttributesByType(inputType);

            return htmlHelper.TextBoxFor(expression, format, attributes);
        }

        public static MvcHtmlString CreateDropDownField<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            if (htmlAttributes != null)
            {
                // Check HTML attributes has specified a class, use them and not our Bootstrap classes.
                if (!attributes.ContainsKey("class"))
                {
                    attributes.SetInputClassAttributesByType(InputType.Default);
                }

                return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attributes);
            }

            attributes.SetInputClassAttributesByType(InputType.Default);

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attributes);
        }

        private static MvcHtmlString CreateValidationMessage<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var attributes = new Dictionary<string, object>();

            attributes.AddClass(ValidationMessageStyleClass);

            return htmlHelper.ValidationMessageFor(expression, null, attributes);
        }

        private static string WrapFormGroup(InputType inputType, string content)
        {
            var html = new StringBuilder();

            // If the input type is a checkbox or radio button, the group uses FormCheckOpenHtml.
            if (inputType == InputType.Checkbox || inputType == InputType.Radio)
            {
                html.Append(FormCheckOpenHtml);
            }
            else
            {
                html.Append(FormGroupOpenHtml);
            }

            html.Append(content);

            html.Append(FormGroupCloseHtml);

            return html.ToString();
        }

        private static void SetInputTypeAttributesByType(this IDictionary<string, object> attributes,
            InputType inputType)
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

        private static void SetInputClassAttributesByType(this IDictionary<string, object> attributes,
            InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Default:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Button:
                    throw new NotImplementedException(TypeExceptionMessage("Button"));

                case InputType.Checkbox:
                    attributes.AddClass(FormCheckInputStyleClass);
                    break;

                case InputType.Color:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Date:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.DateTimeLocal:
                    throw new NotImplementedException(TypeExceptionMessage("DateTimeLocal", "Date"));

                case InputType.Email:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.File:
                    attributes.AddClass(FormControlFileStyleClass);
                    break;

                case InputType.Hidden:
                    throw new InvalidOperationException(TypeExceptionMessage("Hidden", "@Html.HiddenFor()"));

                case InputType.Image:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Month:
                    throw new NotImplementedException(TypeExceptionMessage("Month", "Date"));

                case InputType.Number:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Password:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Radio:
                    attributes.AddClass(FormCheckInputStyleClass);
                    break;

                case InputType.Range:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Reset:
                    throw new InvalidOperationException(TypeExceptionMessage("Reset"));

                case InputType.Search:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Submit:
                    throw new InvalidOperationException(TypeExceptionMessage("Submit", "@Html.Submit()"));

                case InputType.Tel:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Text:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Time:
                    throw new NotImplementedException(TypeExceptionMessage("Time", "Date"));

                case InputType.Url:
                    attributes.AddClass(FormControlStyleClass);
                    break;

                case InputType.Week:
                    throw new NotImplementedException(TypeExceptionMessage("Week", "Date"));

                default:
                    // Should never occur.
                    throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
            }
        }

        private static void SetLabelClassAttributesByType(this IDictionary<string, object> attributes,
            InputType inputType)
        {
            // If the input type is a checkbox or radio button, the label uses FormCheckLabelStyleClass.
            if (inputType == InputType.Checkbox || inputType == InputType.Radio)
            {
                attributes.AddClass(FormCheckLabelStyleClass);
            }
            else
            {
                attributes.AddClass(FormLabelStyleClass);
            }
        }

        private static void AddType(this IDictionary<string, object> attributes, string type)
        {
            attributes.Add("type", type);
        }

        private static void AddClass(this IDictionary<string, object> attributes, string @class)
        {
            attributes.Add("class", @class);
        }

        private static string TypeExceptionMessage(string type, string substitute = null)
        {
            var typeMessage =
                $"{type} is an input type that is not yet implemented or should not be used with @Html.FormGroup() or @Html.FormGroupFor().";

            var substituteMessage = string.Empty;

            if (substitute != null)
                substituteMessage = $"Please use {substitute} instead.";

            var message = typeMessage + " " + substituteMessage;

            return message;
        }

        #endregion

        #endregion

        #region Buttons

        public static MvcHtmlString CreateFormSubmitButton(HtmlHelper helper,
            string value,
            string id = null,
            object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");

            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            attributes.EnsureButtonStyleClass();
            id = EnsureButtonId(id, value);

            builder.Attributes.Add("id", id);
            builder.Attributes.Add("name", id);
            builder.Attributes.Add("type", "submit");
            builder.Attributes.Add("value", value);

            builder.MergeAttributes(attributes);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString CreateJsButton(HtmlHelper htmlHelper, string value, string onClick,
            string id = null, object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");

            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            attributes.EnsureButtonStyleClass();
            id = EnsureButtonId(id, value);

            builder.Attributes.Add("id", id);
            builder.Attributes.Add("name", id);
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("value", value);

            builder.Attributes.Add("onClick", onClick);

            builder.MergeAttributes(attributes);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        private static void EnsureButtonStyleClass(this IDictionary<string, object> attributes)
        {
            // If a particular class is not specified, add a Bootstrap button class.
            if (!attributes.ContainsKey("class"))
            {
                attributes.Add("class", DefaultButtonStyleClass);
            }
        }

        private static string EnsureButtonId(string id, string value)
        {
            // Removes spaces from the value and assigns it as an ID for the submit button if the ID is null.
            // It is necessary for all of our buttons to contain an ID for automation testing.
            return id ?? value.Replace(" ", string.Empty);
        }

        #endregion
    }
}