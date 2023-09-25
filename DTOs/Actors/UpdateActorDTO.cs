using System.ComponentModel.DataAnnotations;

namespace DotnetAppWith.Hangfire.Example.DTOs.Actors
{
    public class UpdateActorDTO
    {
        [MaxLength(10, ErrorMessage = "Name is too long!")]
        public string Name { get; set; }
    }
}
