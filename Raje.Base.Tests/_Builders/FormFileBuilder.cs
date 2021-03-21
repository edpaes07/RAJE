using Bogus;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace Raje.Base.Tests._Builders
{
    public class FormFileBuilder
    {
        protected string Name;
        protected string Content;

        public static FormFileBuilder New()
        {
            var faker = new Faker();
            string name = faker.Person.FullName;
            return new FormFileBuilder
            {
                Name = "teste.txt",
                Content = faker.Lorem.Text()
            };
        }

        public FormFileBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public FormFileBuilder WithContent(string content)
        {
            Content = content;
            return this;
        }

        public IFormFile Build()
        {
            byte[] s_Bytes = Encoding.UTF8.GetBytes(Content);

            return new FormFile(
                baseStream: new MemoryStream(s_Bytes),
                baseStreamOffset: 0,
                length: s_Bytes.Length,
                name: "Data",
                fileName: Name
            );
        }
    }
}
