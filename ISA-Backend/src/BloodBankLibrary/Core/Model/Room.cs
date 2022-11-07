using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Model
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Number { get; set; }
        [Range(1, 10)]
        public int Floor { get; set; }
    }
}
