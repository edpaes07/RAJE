using Raje.DL._Base;
using Raje.DL.DB.Base;
using Raje.Infra.Const;
using System;
using System.ComponentModel.DataAnnotations;

namespace Raje.DL.DB.Admin
{
    public class Media : EntityAuditBase
    {
        [Required]
        [MaxLength(400)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(1000)]
        public string FilePath { get; set; }

        public Media() { }

        public Media(string fileName, string filePath)
        {
            RuleValidation.New()
                .When(string.IsNullOrEmpty(fileName), RajeValidationMessages.FileName)
                .When(string.IsNullOrEmpty(filePath), RajeValidationMessages.FilePath)
                .ThrowException();

            FileName = fileName;
            FilePath = filePath;
            FlagActive = true;
        }

        /// <summary>
        /// Usado apenas no Campanha
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filePath"></param>
        /// <param name="modefiedAt"></param>
        public Media(long id, string filePath, DateTime modefiedAt)
        {
            FilePath = filePath;
            Id = id;
            ModifiedAt = modefiedAt;
        }

        public void SetInfo(string filePath, string fileName)
        {
            FileName = fileName;
            FilePath = filePath;
        }
    }
}
