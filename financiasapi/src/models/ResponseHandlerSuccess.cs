
namespace financiasapi.src.models
{
    public class ResponseHandlerSuccess : ResponseHandler
    {
         public object Data { get; set; }
        public ResponseHandlerSuccess()
        {
            Success = true;
        }
    }
}