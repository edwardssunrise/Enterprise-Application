using System;

namespace EnterpriseArchitecture.Core
{
    /// <summary>
    /// Columns in the table named "Category" in the database are defined as property within this entity class.
    /// </summary>
    public partial class Category : Base
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
    }
}