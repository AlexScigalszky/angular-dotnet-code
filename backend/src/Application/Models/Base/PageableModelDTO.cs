namespace Application.Models.Base
{
    public class PageableModelDTO : BaseDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }

        public PageableModelDTO()
        {
            PageNumber = 1;
            PageSize = 10;
            OrderBy = null;
        }
    }
}
