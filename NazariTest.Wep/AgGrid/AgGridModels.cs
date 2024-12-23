namespace NazariTest.Wep.AgGrid
{
    public class GridRequest
    {
        public int startRow { get; set; }
        public int endRow { get; set; }
        public List<SortModel> SortModel { get; set; }
        public FilterModel FilterModel { get; set; }
    }

    public class SortModel
    {
        public string ColId { get; set; }
        public string Sort { get; set; } // "asc" or "desc"
    }

    public class FilterModel
    {
        public string Make { get; set; }
    }
}
