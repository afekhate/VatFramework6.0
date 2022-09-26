using System;
using System.Collections.Generic;
using System.Text;

namespace VatFramework.Services.Contract
{
    public interface IEntity : IAudit
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        long Id { get; set; }

        DateTime DateCreated { get; set; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();

        /// <summary>
        /// To allow soft delete
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// to mark entity as active or inactive
        /// </summary>
        bool IsActive { get; set; }

    }

    public interface IAudit
    {


        /// <summary>
        /// 
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string DeletedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// to manage versioning
        /// </summary>
        byte[] RowVersion { get; set; }



    }
}
