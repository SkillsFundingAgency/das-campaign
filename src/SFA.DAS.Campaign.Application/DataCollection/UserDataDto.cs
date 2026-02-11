using System;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Application.DataCollection;

[ExcludeFromCodeCoverage]
public class UserDataDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UkEmployerSize { get; set; }
    public string PrimaryIndustry { get; set; }
    public string PrimaryLocation { get; set; }
    public DateTime AppsgovSignUpDate { get; set; }
    public string PersonOrigin { get; set; }
    public bool IncludeInUR { get; set; }
}