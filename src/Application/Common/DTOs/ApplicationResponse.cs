namespace BookStoreApi.Application.Common.DTOs;

public class ApplicationResponse<T>
{
    public ApplicationResponse()
    {
        this.Errors = new Dictionary<string, string>();
    }
    
    private T? Content { get; set; }
    private IDictionary<string, string>? Errors { get; set; }
    
    public void SetContentValue(T content)
    {
        this.Content = content;
    }

    public void AddError(string code, string error)
    {
        this.Errors?.Add(code, error);
    }
}