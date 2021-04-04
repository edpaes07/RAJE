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

        [Required]
        [MaxLength(200)]
        public string Folder { get; set; }

        public void SetInfo(string filePath, string fileName, string folder)
        {
            FileName = fileName;
            FilePath = filePath;
            Folder = folder;
        }

        public Media() { }

        public Media(string fileName, string filePath, string folder)
        {
            RuleValidation.New()
                .When(string.IsNullOrEmpty(fileName), ValidationMessages.FileName)
                .When(string.IsNullOrEmpty(filePath), ValidationMessages.FilePath)
                .When(string.IsNullOrEmpty(folder), ValidationMessages.Folder)
                .ThrowException();

            FileName = fileName;
            FilePath = filePath;
            Folder = folder;
            FlagActive = true;
        }
    }
}