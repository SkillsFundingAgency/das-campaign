#nullable enable
using MediatR;
using System;

namespace SFA.DAS.Campaign.Application.FundingTool.Queries.Calculation
{
    public class CalculationQueryResult : IRequest<CalculationQuery>
    {
        public int Funding { get; set; }
        public string FundingOutput { get { return String.Format("{0:#,##0.##}", Funding); } }
        public int? Training { get; set; }
        public string? TrainingOutput { get { return String.Format("{0:#,##0.##}", Training); } }
        public int Duration { get; set; }
        public int Level { get; set; }
        public string Title { get; set; } = null!;

        //public string? Format(int? number)
        //{
        //    if (number == null) { return null; }

        //    var newNumber = number.Value.ToString();

        //    switch (newNumber.Length)
        //    {
        //        case 4:
        //            return newNumber.Insert(1, ",");
        //        case 5:
        //            return newNumber.Insert(2, ",");
        //        case 6:
        //            return newNumber.Insert(3, ",");
        //        case 7:
        //            newNumber = newNumber.Insert(1, ",");
        //            newNumber = newNumber.Insert(5, ",");
        //            return newNumber;
        //        default:
        //            return newNumber;

        //    }
        //}

    }
}
