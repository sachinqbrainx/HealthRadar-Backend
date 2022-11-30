namespace Revenue.CommandModel
{
    public class PieCommandModel
    {
        public List<Chart> PieChart { get; set; }

    }

    public class Chart
    {
        public string Department { get; set; }
        public string NoOfPerson { get; set; }
        public string Text { get; set; }
    }

}
