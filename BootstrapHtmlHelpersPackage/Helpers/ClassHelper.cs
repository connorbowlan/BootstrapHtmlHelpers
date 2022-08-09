namespace BootstrapHtmlHelpersPackage.Helpers
{
    public static class ClassHelper
    {
        // HTML and CSS classes used to construct a standard form group, or a form group for a check box or radio button.
        public const string FormGroupOpenHtml = "<div class=\"form-group\">";
        public const string FormCheckOpenHtml = "<div class=\"form-group form-check\">";
        public const string FormGroupCloseHtml = "</div>";

        // CSS classes used to style a standard input label, or an input label for a check box or radio button.
        public const string FormLabelStyleClass = "form-label";
        public const string FormCheckLabelStyleClass = "form-check-label";

        // CSS classes used to style a standard input field, or any other type potentially specified.
        public const string FormControlStyleClass = "form-control";
        public const string FormCheckInputStyleClass = "form-check-input";
        public const string FormControlFileStyleClass = "form-control-file";

        // CSS classes used to style validation results via jQuery Unobtrusive Validation.
        public const string FormValidationMessageStyleClass = "text-danger";

        // CSS classes used to style buttons.
        public const string FormDefaultButtonStyleClass = "btn btn-primary";
    }
}
