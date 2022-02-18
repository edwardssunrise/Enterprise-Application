using System;

namespace EnterpriseArchitecture.DTO.EEntity
{
    /// <summary>
    /// The Data Transfer Object (DTO) layer is used to prevent all table 
    /// columns in the database tables from being sent to the client, such as:
    ///
    /// 1. To hide features that clients should not display.
    /// 2. Reducing the load size.
    /// 3. Over-send to avoid security vulnerabilities.
    /// 4. Separating the Service Layer from the database.
    /// </summary>
    public class EPostUserDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string PostImageURL { get; set; }
        public string UserImageURL { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string Color { get; set; }
    }
}