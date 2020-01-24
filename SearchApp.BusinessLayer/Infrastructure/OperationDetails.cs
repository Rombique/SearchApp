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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (!(obj is OperationDetails))
                return false;
            OperationDetails od = obj as OperationDetails;

            return Equals(Result, od.Result); //TODO: опасная тема
        }

        public override int GetHashCode()
        {
            int? hashCode = Succeedeed.GetHashCode() ^ Message?.GetHashCode() ^ Category?.GetHashCode();
            if (ReferenceEquals(Result, null))
                return hashCode ?? Succeedeed.GetHashCode();
            else
                return Result.GetHashCode() ^ hashCode ?? Succeedeed.GetHashCode();
        }
    }
}
