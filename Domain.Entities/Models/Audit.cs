using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Resources.Constants;

namespace Domain.Entities.Models
{
    public class Audit
    {
        [Column("IsActive")]
        [Required]
        public bool IsActive { get; set; }

        [Column("CreatedByUser")]
        [Required]
        private long _createdByUser;
        public long CreatedByUser
        {
            get
            {
                return _createdByUser == DomainConstants.DEFAULT_ID ?
                    DomainConstants.ADMIN_USER : _createdByUser;
            }
            set
            {
                _createdByUser = value;
            }
        }

        [Column("CreatedDate")]
        [Required]
        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate == DateTime.MinValue ?
                    DateTime.Now : _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        [Column("UpdatedByUser")]
        [Required]
        private long _updatedByUser;
        public long UpdatedByUser
        {
            get
            {
                return _updatedByUser == DomainConstants.DEFAULT_ID ?
                    DomainConstants.ADMIN_USER : _updatedByUser;
            }
            set
            {
                _updatedByUser = value;
            }
        }

        [Column("UpdatedDate")]
        [Required]
        private DateTime _updatedDate;
        public DateTime UpdatedDate
        {
            get
            {
                return _updatedDate == DateTime.MinValue ?
                    DateTime.Now : _updatedDate;
            }
            set
            {
                _updatedDate = value;
            }
        }
    }
}
