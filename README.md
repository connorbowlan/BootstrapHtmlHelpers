# Bootstrap HTML Helpers
A library of HTML Helpers containing Bootstrap 4.6 attributes.

08/08/2022 - Alpha 0.1.0

Bootstrap HTML Helpers is used to generate a Bootstrap 4.6 form group by a given input type containing:
1. A label for an input field/form control.
2. An input field/form control.
3. A validation message created by jQuery Unobtrusive Validation for that input field.
    
Applying data annotations to the property being passed will create validation restrictions for the field.

This class utilizes Microsoft's InputExtensions/SelectExtensions (used in HTML Helpers) with a combination of custom attributes for Bootstrap and field types passed via parameters.

Features:
- Uses data annotations to power active validation.
- Populates buttons with ID attributes with the value(required) given after removing the spaces.
- Applies styles defined in the constants section to their respective elements.

Requirements:
- Bootstrap 4.6 (and any of its dependencies)
- jQuery
- jQuery Validation
- jQuery Validation Unobtrusive
