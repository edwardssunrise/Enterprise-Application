namespace EnterpriseArchitecture.Core
{
    /// <summary>
    /// Columns in the table named "User" in the database are defined as property within this entity class.
    /// </summary>
    public partial class User : Base
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public byte[] Image { get; set; }
    }
}