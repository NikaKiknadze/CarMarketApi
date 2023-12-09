using System.ComponentModel.DataAnnotations;

namespace CarMarketApi.Dtos.PersonalInformationDtos
{
    public class PersonalInformationOnlyDto
    {
        public int? Id { get; set; }
        
        public int? PhoneNumber { get; set; }
        
        public string? EmailAddress { get; set; }
    }
}
