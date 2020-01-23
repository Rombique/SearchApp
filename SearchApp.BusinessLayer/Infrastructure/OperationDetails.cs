namespace SearchApp.BusinessLayer.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succeedeed, string message, string category, object result = null)
        {
            Succeedeed = succeedeed;
            Message = message;
            Category = category;
            Result = result;
        }
        public bool Succeedeed { get; private set; }
        public string Message { get; private set; }
        public string Category { get; private set; }
        public object Result { get; private set; }
    }
}
