using System;

namespace EnterpriseArchitecture.Core
{
    /// <summary>
    /// Columns in the table named "Post" in the database are defined as property within this entity class.
    /// </summary>
    public partial class Post : Base
    {
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public byte[] Image { get; set; }
        public string ShortDescription { get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}