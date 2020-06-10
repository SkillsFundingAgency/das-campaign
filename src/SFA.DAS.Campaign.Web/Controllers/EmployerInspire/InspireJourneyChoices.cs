using System.Collections.Generic;
using SFA.DAS.Campaign.Web.Controllers.EmployerInform;

namespace SFA.DAS.Campaign.Web.Controllers.EmployerInspire
{
    public class InspireJourneyChoices
    {
        public InspireJourneyChoices()
        {
            SelectedSkills = new List<string>();
            LevyOption = new LevyOption();
        }
        
        public List<Skill> Skills =>
            new List<Skill>
            {
                new Skill {Key = "agriculture-environment-and-animal-care", Id = "agriculture", Title = "Agriculture, environment and animal care", Examples = "Land-based service engineering technician, Sports turf operative, Horticulture and Landscape Operative"},
                new Skill {Key = "business-and-administration", Id = "business", Title = "Business and administration", Examples = "HR Consultant / Partner, Business Administrator, Public service operational delivery officer"},
                new Skill {Key = "care-services", Id = "care", Title = "Care services", Examples = "Adult care worker, Social worker"},
                new Skill {Key = "catering-and-hospitality", Id = "catering", Title = "Catering and hospitality", Examples = "Chef De Partie, Advanced butcher, Hospitality supervisor"},
                new Skill {Key = "construction", Id = "construction", Title = "Construction", Examples = "Building Services Design Technician, Building Services Engineering Ductwork Craftsperson, Digital Engineering Technician"},
                new Skill {Key = "creative-and-design", Id = "creative", Title = "Creative and design", Examples = "Broadcast and communications technical operator, Cultural learning and participation officer, Jewellery Maker"},
                new Skill {Key = "digital", Id = "digital", Title = "Digital", Examples = "IS Business Analyst, Software Development Technician, Cybersecurity technologist"},
                new Skill {Key = "education-and-childcare", Id = "education", Title = "Education and childcare", Examples = "Teacher, Children, Young People & Families Manager, Learning mentor"},
                new Skill {Key = "engineering-and-manufacturing", Id = "engineering", Title = "Engineering and manufacturing", Examples = "Electrical Power Networks Engineer, Metal casting, foundry & patternmaking technician, Brewer"},
                new Skill {Key = "hair-and-beauty", Id = "beauty", Title = "Hair and beauty", Examples = "Hair Professional, Beauty therapist, Nail services technician"},
                new Skill {Key = "health-and-science", Id = "health", Title = "Health and science", Examples = "Dental technician, Laboratory scientist, Food technologist"},
                new Skill {Key = "legal-finance-and-accounting", Id = "legal", Title = "Legal, finance and accounting", Examples = "Professional accounting / taxation technician, Credit controller / collector, Mortgage adviser"},
                new Skill {Key = "protective-services", Id = "protective", Title = "Protective services", Examples = "HM Forces serviceperson (public services), Business Fire Safety Advisor, Custody & Detention Officer"},
                new Skill {Key = "sales-marketing-and-procurement", Id = "sales", Title = "Sales, marketing and procurement", Examples = "Travel consultant, Fishmonger, Customer service practitioner"},
                new Skill {Key = "transport-and-logistics", Id = "transport", Title = "Transport and logistics", Examples = "Rail Infrastructure Operator, Passenger transport driver - bus, coach and tram, Cabin Crew"}
            };

        public List<string> SelectedSkills { get; set; }
        public LevyOption LevyOption { get; set; }
        public HireSomeoneOptions? HireSomeoneOptions { get; set; }
        public string Postcode { get; set; }
    }

    public class Skill
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Examples { get; set; }
    }
}