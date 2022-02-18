namespace EnterpriseArchitecture.Utilities.SessionOperations
{
    /// <summary>
    /// The data model to be used at login is encapsulated by this class.
    /// </summary>
    public class SessionContext
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string ImageURL { get; set; }
    }
}