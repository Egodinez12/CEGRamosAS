namespace ML
{
    public class Result
    {
        public bool Correct { get; set; }
        public string MessangeError { get; set; }
        public Exception ex { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }


    }
}