using humanamente.Models;
using Xunit;

namespace humanamente.Testes
{
    public class ProfessionTests
    {
        [Fact]
        public void Should_Create_Profession()
        {
            var p = new Profession
            {
                Title = "Psicólogo",
                Description = "Atendimento humano"
            };

            Assert.Equal("Psicólogo", p.Title);
        }
    }
}
