using Raje.DL.Services.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Raje.DL.DB.Base
{
    /// <summary>
    /// Classe de exemplo para ser usada como entidade básica para definir campos padrões.
    /// </summary>
    public abstract class EntityAuditBase : EntityBase, IEntityAudit
    {
        ///<summary>Login do usuário que efetuou a inclusão </summary>
        [Required]
        [MaxLength(500)]
        public string CreatedBy { get; set; }

        ///<summary>Data de inclusão do registro </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        ///<summary>Login do usuário que efetuou a alteração </summary>
        [Required]
        [MaxLength(500)]
        public string ModifiedBy { get; set; }

        ///<summary>Data de alteração do registro </summary>
        [Required]
        public DateTime ModifiedAt { get; set; }

    }
}