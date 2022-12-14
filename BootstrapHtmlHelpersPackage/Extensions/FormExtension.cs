using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using BootstrapHtmlHelpersPackage.Builders;
using InputType = BootstrapHtmlHelpersPackage.Enums.InputType;

namespace BootstrapHtmlHelpersPackage.Extensions
{
    public static class FormExtension
    {
        #region FormSubmitButton

        /// <summary>
        /// Returns an HTML 
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="value">The value and text to be displayed of the form submit button.</param>
        /// <param name="id">The ID of the form submit button.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns></returns>
        public static MvcHtmlString FormSubmitButton(this HtmlHelper htmlHelper, string value, string id = null, object htmlAttributes = null)
        {
            return FormBuilder.CreateFormSubmitButton(htmlHelper, value, id, htmlAttributes);
        }

        #endregion

        #region FormGroupFor

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="inputType"></param>
        /// <param name="format">A string that is used to format the input.</param>
        /// <param name="helpText"></param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">The type of <see cref="TProperty"/> is not supported.</exception>
        public static MvcHtmlString FormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            InputType inputType = InputType.Default,
            string format = null,
            string helpText = null,
            object htmlAttributes = null)
        {
            // If an input type is passed, use that input type.
            if (inputType != InputType.Default)
            {
                return FormBuilder.CreateFormGroup(htmlHelper, expression, inputType, format, helpText, htmlAttributes);
            }

            string propertyType;

            var dataType = typeof(TProperty);

            var nullableType = Nullable.GetUnderlyingType(dataType);

            // Property is a nullable data type. Assess and get the base type.
            if (nullableType?.FullName != null)
            {
                propertyType = nullableType.FullName.Remove(0, 7);
            }
            else
            {
                propertyType = dataType.Name;
            }

            switch (propertyType)
            {
                case "Char":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Text, format, helpText, htmlAttributes);

                case "String":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Text, format, helpText, htmlAttributes);

                case "Int32":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, helpText, htmlAttributes);

                case "Double":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, helpText, htmlAttributes);

                case "Int64": // Long
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, helpText, htmlAttributes);

                case "Single": // Float
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, helpText, htmlAttributes);

                case "Boolean":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Checkbox, format, helpText, htmlAttributes);

                case "DateTime":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Date, format, helpText, htmlAttributes);
            }

            throw new InvalidOperationException(
                $"The data type \"{dataType}\" the property you are passing to @Html.FormGroupFor() is not supported.");
        }


        public static MvcHtmlString FormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            string helpText = null,
            object htmlAttributes = null)
        {
            return FormBuilder.CreateFormGroup(htmlHelper, expression, selectList, optionLabel, helpText, htmlAttributes);
        }

        // TODO: Determine a good way to implement EnumDropDownListFor().
        public static MvcHtmlString FormGroupFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression,
            string optionLabel = null,
            string helpText = null,
            object htmlAttributes = null)
        {
            return FormBuilder.CreateFormGroup(htmlHelper, expression, optionLabel, helpText, htmlAttributes);
        }

        #endregion
    }
}