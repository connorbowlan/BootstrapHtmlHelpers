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