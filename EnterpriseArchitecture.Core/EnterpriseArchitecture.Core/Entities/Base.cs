using System;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseArchitecture.Core
{
    /// <summary>
    /// The abstract class has been defined so that no objects can be created using the base class.
    /// 
    /// [Key] data annotation indicates that the properties will be an ID in the database.
    /// 
    /// Base class ensures that the "ID" column is mandatory in every table that will take place in the database.
    /// </summary>
    public abstract class Base
    {
        [Key]
        public int ID { get; set; }
    }
}