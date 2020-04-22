namespace PizzaDotNet.Data.Models.DTO
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Common.Models;

    public class SessionCartDto : BaseDeletableModel<int>
    {
        public SessionCartDto()
        {
            this.Products = new HashSet<SessionCartProductDto>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<SessionCartProductDto> Products { get; set; }
    }
}
