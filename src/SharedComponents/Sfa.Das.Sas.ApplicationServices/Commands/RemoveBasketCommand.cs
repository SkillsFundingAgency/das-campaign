using System;
using MediatR;

namespace Sfa.Das.Sas.ApplicationServices.Commands
{
    public class RemoveBasketCommand : IRequest<Guid>
    {
        public Guid? BasketId { get; set; }
    }
}
