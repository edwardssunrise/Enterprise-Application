using System;

namespace EnterpriseArchitecture.DTO.EEntity
{
    /// <summary>
    /// The Data Transfer Object (DTO) layer is used to prevent all table columns 
    /// in the database tables from being sent to the client, such as:
    ///
    /// 1. To hide features that clients should not display.
    /// 2. Reducing the load size.
    /// 3. Over-send to avoid security vulnerabilities.
    /// 4. Separating the Service Layer from the database.
    /// </summary>
    public class EPostDetailDTO
    {  
        public string Title { get; set; }
        public string PostContent { get; set; }
        public string PostImageUrl { get; set; }
        public string UserImageUrl { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; }
    }
}