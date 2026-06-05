using AtelierTest.DTOs;
using Swashbuckle.AspNetCore.Filters;

public class PlayerExample : IExamplesProvider<CreatePlayerDto>
{
    public CreatePlayerDto GetExamples()
    {
        return new CreatePlayerDto
        {
            Firstname = "Ons",
            Lastname = "Jabeur",
            Shortname = "O.JAB",
            Sex = "F",
            CountryCode = "TUN",
            CountryPicture = "https://flagcdn.com/tun.png",
            Picture = "https://example.com/jabeur.png",
            Rank = 2,
            Points = 7500,
            Weight = 66,
            Height = 167,
            Age = 29
        };
    }
}