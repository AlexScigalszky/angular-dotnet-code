namespace Application.Models.Base
{
    public class PageableDTO : BaseDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }

        public PageableDTO()
        {
            PageNumber = 1;
            PageSize = 10;
            OrderBy = null;
        }
    }
}
