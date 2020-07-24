namespace Umbraco.Plugins.Connector.Models
{
    using System;
    using System.Collections.Generic;
    public class ValidationErrorList
    {
        public List<ValidationErrorItem> Errors { get; }
        public ValidationErrorList() { Errors = new List<ValidationErrorItem>(); }

        public void AddError(ValidationErrorItem item)
        {
            Errors.Add(item);
        }

        public void AddError(string location, int row, Exception expected = null)
        {
            Errors.Add(new ValidationErrorItem { Location = location, Row = row, Exception = expected });
        }
    }
}
