using System.ComponentModel.DataAnnotations;

namespace SmartWorkout.DTO
{
    public class ExerciseDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
