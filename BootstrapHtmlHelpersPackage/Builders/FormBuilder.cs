using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BootstrapHtmlHelpersPackage.Helpers;
using InputType = BootstrapHtmlHelpersPackage.Enums.InputType;

namespace BootstrapHtmlHelpersPackage.Builders
{
    public static class FormBuilder
    {
        #region Form Group

        public static MvcHtmlString CreateFormGroup<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            InputType inputType,
            string format = null,
            string helpText = null,
            object htmlAttributes = null)
        {
            var label = CreateLabel(htmlHelper, expression, inputType);
            var inputField = CreateFormControl(htmlHelper, expression, inputType, format, htmlAttributes);

            if (helpText != null)
            {
                var id = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;
                helpText = CreateHelpText(id, helpText);
            }

            var validationMessage = CreateValidationMessage(htmlHelper, expression);

            string content;

            // If the input type is a checkbox or a radio button, the label comes second.
            if (inputType == InputType.Checkbox || inputType == InputType.Radio)
            {
                content = inputField.ToString() + label + helpText + validationMessage;
            }
            else
            {
                content = label + inputField.ToString() + helpText + validationMessage;
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
            var dropdownField = CreateDropDown(htmlHelper, expression, selectList, optionLabel, htmlAttributes);
            var validationMessage = CreateValidationMessage(htmlHelper, expression);

            var content = label + dropdownField.ToString() + validationMessage;

            var formGroup = WrapFormGroup(InputType.Default, content);

            return MvcHtmlString.Create(formGroup);
        }

        #endregion

        #region Label

        private static MvcHtmlString CreateLabel<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, InputType inputType)
        {
            var attributes = new Dictionary<string, object>();

            attributes.SetLabelClassAttributesByType(inputType);

            // TODO: Assess whether this is actually necessary to do with W3 Standards.
            //var id = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;
            //attributes.SetAccessibilityAttributesByType(id, AccessibilityType.Label);

            return htmlHelper.LabelFor(expression, attributes);
        }

        #endregion

        #region Form Control

        private static MvcHtmlString CreateFormControl<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
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

            // TODO: Assess whether this is actually necessary to do with W3 Standards.
            //var id = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;
            //attributes.SetAccessibilityAttributesByType(id, AccessibilityType.FormControl);

            return htmlHelper.TextBoxFor(expression, format, attributes);
        }

        #endregion

        #region Drop Down

        public static MvcHtmlString CreateDropDown<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
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

        #endregion

        #region Help Text

        public static string CreateHelpText(string id, string helpText)
        {
            var tagBuilder = new TagBuilder("small");

            tagBuilder.MergeAttribute("id", id);
            tagBuilder.MergeAttribute("class", "form-text");
            tagBuilder.SetInnerText(helpText);

            return tagBuilder.ToString();

        }

        #endregion

        #region Validation Message

        private static MvcHtmlString CreateValidationMessage<TModel, TProperty>(HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            var attributes = new Dictionary<string, object>();

            attributes.AddClass(ClassHelper.FormValidationMessageStyleClass);

            return htmlHelper.ValidationMessageFor(expression, null, attributes);
        }

        private static string WrapFormGroup(InputType inputType, string content)
        {
            var html = new StringBuilder();

            // If the input type is a checkbox or radio button, the group uses FormCheckOpenHtml.
            if (inputType == InputType.Checkbox || inputType == InputType.Radio)
            {
                html.Append(ClassHelper.FormCheckOpenHtml);
            }
            else
            {
                html.Append(ClassHelper.FormGroupOpenHtml);
            }

            html.Append(content);

            html.Append(ClassHelper.FormGroupCloseHtml);

            return html.ToString();
        }

        #endregion

        #region Submit Button

        public static MvcHtmlString CreateFormSubmitButton(HtmlHelper helper,
            string value,
            string id = null,
            object htmlAttributes = null)
        {
            var builder = new TagBuilder("input");

            var attributes = HtmlHelper.ObjectToDictionary(htmlAttributes);

            attributes.EnsureButtonStyleClass();
            id = AttributeHelper.EnsureId(id, value);

            builder.Attributes.Add("id", id);
            builder.Attributes.Add("name", id);
            builder.Attributes.Add("type", "submit");
            builder.Attributes.Add("value", value);

            builder.MergeAttributes(attributes);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        #endregion
    }
}