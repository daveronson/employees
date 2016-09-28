namespace EmployeeDirectory.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Razor;

    public class FeatureFolderViewExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context) { }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            return viewLocations
                .Select(f => f.Replace("/Views/", "/Features/"));
        }
    }
}